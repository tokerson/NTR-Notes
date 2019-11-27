using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Z02.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using LinqKit;
using Microsoft.Data.SqlClient;

namespace Z02.Controllers
{
    public class NotesController : Controller
    {
        private HashSet<string> allCategories = new HashSet<string>() { "All" };
        private int pageSize = 3;

        public async Task<IActionResult> Index(DateTime start_date, DateTime last_date, int? pageNumber, string btnSubmit, string chosenCategory = "All")
        {

            using(var context = new DBContext()) {
                var categories = context.Categories.AsNoTracking();
                var notes = context.Notes.AsQueryable();
                ViewData["Categories"] = await categories.ToListAsync();

            if (last_date == DateTime.MinValue)
            {
                last_date = DateTime.MaxValue;
            }
            else
            {
                TempData["lastDate"] = last_date.ToString("yyyy-MM-dd");
            }

            if (start_date != DateTime.MinValue)
            {
                TempData["startDate"] = start_date.ToString("yyyy-MM-dd");
            } else if (Convert.ToDateTime(TempData.Peek("startDate")) != DateTime.MinValue)
            {
                start_date = Convert.ToDateTime(TempData.Peek("startDate"));
            }

            if (btnSubmit == "Clear")
            {
                TempData.Clear();
                var Notes = from n in context.Notes select n;
                return View(await PaginatedList<Note>.CreateAsync(Notes.AsNoTracking(), pageNumber ?? 1, pageSize));
            } 

            notes = notes.Where(n => n.NoteDate.Date <= last_date && n.NoteDate.Date  >= start_date);
            
            if (chosenCategory != null && chosenCategory != "All")
            {
                notes = notes.Where(n => n.NoteCategories.Any(c => c.Category.Title == chosenCategory));
            }

            TempData["chosenCategory"] = chosenCategory;
            TempData["pageNumber"] = pageNumber ?? 1;
            TempData.Keep("chosenCategory");
            TempData.Keep("startDate");
            TempData.Keep("lastDate");
            TempData.Keep("pageNumber");

            return View(await PaginatedList<Note>.CreateAsync(notes.AsNoTracking(), pageNumber ?? 1, pageSize));
            }
        }

        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            using(var context = new DBContext()){
                var note = await context.Notes.Include(i => i.NoteCategories).ThenInclude(noteCategories => noteCategories.Category).FirstOrDefaultAsync(note => note.NoteID == id);
                return View(note);
            }
        }

        public IActionResult New() {
            var note = new Note();
            note.NoteCategories = new List<NoteCategory>();
            note.NoteDate = DateTime.Now;
            return View(note);
        }

        [HttpPost]
        public async Task<IActionResult> New(Note note,String[] categories, string category = "", string btnSubmit = "")
        {
            category = category ?? "";
            category = category.Trim();
            try
            {
                switch(btnSubmit){
                    case "Add":
                        if(note.NoteCategories == null) {
                            note.NoteCategories = new List<NoteCategory>();
                        }
                        note.NoteCategories.Add(new NoteCategory{Category=new Category{Title=category}});
                        Array.ForEach(categories, c => note.NoteCategories.Add(new NoteCategory { Category = new Category{Title=c}}));
                        ModelState.Clear();
                        return View(note);
                    case "Remove":
                        var remainingCategories = categories.Where(c => c != category).ToArray();
                        Array.ForEach(remainingCategories, c => note.NoteCategories.Add(new NoteCategory { Category = new Category{Title=c}}));
                        ModelState.Clear();
                        return View(note);
                }
                if (ModelState.IsValid)
                {
                    using(var context = new DBContext()){
                        if(note.NoteCategories == null) {
                            note.NoteCategories = new List<NoteCategory>();
                        }
                        context.Notes.Add(note);
                        Array.ForEach(categories, c => {
                            try {
                                //create 2 variants, one for new category and another for existing category
                                Category cat;
                                using(var transaction = context.Database.BeginTransaction()){
                                    var occurances = context.Categories.Where(categoryObj => categoryObj.Title == c).ToList();
                                    if(occurances.Count() == 0) {
                                        cat = new Category {Title = c};
                                        context.Categories.Add(cat);
                                    } else {
                                        cat = occurances.FirstOrDefault();
                                    }
                                    context.SaveChanges();
                                    note.NoteCategories.Add(new NoteCategory {
                                    NoteID=note.NoteID,
                                    CategoryID = cat.CategoryID});
                                    context.SaveChanges();
                                    transaction.Commit();
                                }
                            } catch (DbUpdateException e) {
                                ModelState.AddModelError("", "Unable to save changes. " +
                                    "Try again, and if the problem persists " +
                                    "see your system administrator.");
                            }
                        });
                        await context.SaveChangesAsync();
                        return returnToIndex();
                    }
                }
            }
            catch (DbUpdateException e)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }

            return View(note);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int? id, byte[] rowVersion, string category = "", string btnSubmit = "")
        {
            if (id == null)
            {
                return NotFound();
            }

            using(var context = new DBContext()){
                var noteToUpdate = await context.Notes.Include(i => i.NoteCategories).ThenInclude(noteCategories => noteCategories.Category).FirstOrDefaultAsync(note => note.NoteID == id);

                category = category ?? "";
                category = category.Trim();

                if(noteToUpdate == null){
                    TempData["Error"] = "Unable to save changes. The Note was deleted by another user.";
                    TempData.Keep("Error");
                    return returnToIndex();
                }

                context.Entry(noteToUpdate).Property("RowVersion").OriginalValue = rowVersion;

                if (btnSubmit != "")
                {
                    var occurances = context.Categories.Where(c => c.Title == category).ToList();
                    var categoryObj = await context.Categories.FirstOrDefaultAsync(c => c.Title == category);


                    if(btnSubmit == "Add") {
                        if (category.Length > 0)
                        {
                            if(occurances.Count == 0) {
                                context.Categories.Add(new Category{ Title = category});
                                await context.SaveChangesAsync();
                                categoryObj = await context.Categories.FirstOrDefaultAsync(c => c.Title == category);
                            }
                            if(context.NoteCategories.Where(nc => nc.Category.Title == category && nc.NoteID == noteToUpdate.NoteID).ToList().Count == 0){
                                noteToUpdate.NoteCategories.Add(new NoteCategory{ CategoryID = categoryObj.CategoryID, NoteID = noteToUpdate.NoteID });
                                await context.SaveChangesAsync();
                            }
                            ModelState.Clear();
                        }
                        return View(noteToUpdate);
                    } else if (btnSubmit == "Remove") {
                        if (occurances.Count > 0)
                        {
                            if(context.NoteCategories.Where(nc => nc.Category.Title == category && nc.NoteID == noteToUpdate.NoteID).ToList().Count == 0) {

                            } else {
                                noteToUpdate.NoteCategories.Remove(new NoteCategory{ CategoryID = categoryObj.CategoryID, NoteID = noteToUpdate.NoteID });
                                await context.SaveChangesAsync();
                                if(occurances.Count == 1) {
                                    context.Categories.Remove(categoryObj);
                                    await context.SaveChangesAsync();
                                }
                            }
                            ModelState.Clear();
                        }
                        return View(noteToUpdate);
                    }
                }

                if(await TryUpdateModelAsync<Note>(
                        noteToUpdate,
                        "",
                        n => n.Title, n => n.Description, n => n.NoteDate
                    ))
                    {
                        try
                        {
                            await context.SaveChangesAsync();
                            return returnToIndex();
                        } catch (DbUpdateConcurrencyException ex)
                        {
                            var exceptionEntry = ex.Entries.Single();
                            var databaseEntry = exceptionEntry.GetDatabaseValues();
                            if(databaseEntry == null)
                            {
                                ModelState.AddModelError(string.Empty, "Unable to save changes. The Note was deleted by another user.");
                            } else {
                                ModelState.AddModelError(string.Empty, "The record you attempted to edit "
                                + "was modified by another user after you got the original value. The "
                                + "edit operation was canceled and the current values in the database "
                                + "have been displayed. If you still want to edit this record, click "
                                + "the Save button again. Otherwise click the Back to List hyperlink.");
                                noteToUpdate.RowVersion = (byte[])noteToUpdate.RowVersion;
                                ModelState.Remove("RowVersion");
                            }
                        } catch (DbUpdateException ex)
                        {
                            ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                        }

                    }
            return View(noteToUpdate);
            }
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null){
                return NotFound();
            }

            using(var context = new DBContext()){
                using(var transaction = context.Database.BeginTransaction()){
                    try {
                            var note = await context.Notes.Include(i => i.NoteCategories).ThenInclude(noteCategories => noteCategories.Category).FirstOrDefaultAsync(note => note.NoteID == id);
                            Category[] categories = note.NoteCategories.Select(nc => nc.Category).ToArray();
                            context.Notes.Attach(note);
                            context.Notes.Remove(note);
                            // remove all categories not used by any other category
                            context.SaveChanges();
                            Array.ForEach(categories, c => {
                                if(context.NoteCategories.Where(nc => nc.Category == c).ToList().Count == 0){
                                    context.Categories.Remove(c);
                                }
                            });
                            context.SaveChanges();
                            transaction.Commit();
                    } catch(DbUpdateConcurrencyException) {
                        ModelState.AddModelError("Title","Something went wrong with deleting");
                    }
                }
            }
            return returnToIndex();
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private RedirectToActionResult returnToIndex()
        {
            RouteValueDictionary dict = new RouteValueDictionary();
            dict.Add("chosenCategory", TempData.Peek("chosenCategory"));
            dict.Add("start_date", Convert.ToDateTime(TempData.Peek("startDate")));
            dict.Add("last_date", Convert.ToDateTime(TempData.Peek("lastDate")));
            dict.Add("pageNumber",TempData.Peek("pageNumber"));

            return RedirectToAction(nameof(Index), dict);
        }

    }
}
