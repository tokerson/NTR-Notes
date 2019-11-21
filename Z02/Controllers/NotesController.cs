using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Z02.Repositories;
using Z02.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

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

                ViewData["Categories"] = await categories.ToListAsync();

            if (btnSubmit == "Clear")
            {
                TempData.Clear();
                return View(new PaginatedList<Note>(await context.Notes.AsNoTracking().ToListAsync(), pageNumber ?? 1, pageSize));
            }

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

            var notes = context.Notes.Where(note => note.NoteDate >= start_date && note.NoteDate <= last_date);

            if (chosenCategory != null && chosenCategory != "All")
            {

                // notes = notes.Where(note => note.NoteCategories.Contains(chosenCategory)).ToList();
            }

            TempData["chosenCategory"] = chosenCategory;
            TempData["pageNumber"] = pageNumber ?? 1;
            TempData.Keep("chosenCategory");
            TempData.Keep("startDate");
            TempData.Keep("lastDate");
            TempData.Keep("pageNumber");

            return View(new PaginatedList<Note>(await notes.ToListAsync(), pageNumber ?? 1, pageSize));
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
            return View(note);
        }

        [HttpPost]
        public async Task<IActionResult> New(Note note, string category = "", string btnSubmit = "")
        {
            category = category ?? "";
            category = category.Trim();
            try
            {
                switch(btnSubmit){
                    case "Add":
                        break;
                    case "Remove":
                        break;
                }
                if (ModelState.IsValid)
                {
                    using(var context = new DBContext()){
                        context.Add(note);
                        await context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
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
        // [ValidateAntiForgeryToken]
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
                    ModelState.AddModelError(string.Empty,
                        "Unable to save changes. The department was deleted by another user.");
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
                        n => n.Title, n => n.Description, n => n.NoteCategories, n => n.NoteDate
                    ))
                    {
                        try
                        {
                            await context.SaveChangesAsync();
                            return returnToIndex();
                        } catch (DbUpdateConcurrencyException ex)
                        {
                            Console.WriteLine(ex.Message);
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
                try {
                    var note = await context.Notes.FirstOrDefaultAsync(note => note.NoteID == id);
                    context.Notes.Attach(note);
                    context.Notes.Remove(note);
                    await context.SaveChangesAsync();
                } catch(DbUpdateConcurrencyException e) {
                    ModelState.AddModelError("Title","Something went wrong with deleting");
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
