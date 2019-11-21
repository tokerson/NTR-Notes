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
                var note = context.Notes.FirstOrDefaultAsync(note => note.NoteID == id);
                return View(await note);
            }
        }

        public async Task<IActionResult> New(Note note, string category = "", string btnSubmit = "")
        {
            category = category ?? "";
            category = category.Trim();
            try
            {
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
        public async Task<IActionResult> Edit(int id, Note note, string category = "", string btnSubmit = "")
        {
            if (id != note.NoteID)
            {
                return NotFound();
            }

            category = category ?? "";
            category = category.Trim();

            // switch (btnSubmit)
            // {
            //     case "Add":
            //         if (category.Length > 0)
            //         {
            //             note.categories.Add(category);
            //             ModelState.Clear();
            //         }
            //         return View(note);
            //     case "Remove":
            //         if (note.categories.Contains(category))
            //         {
            //             note.categories.Remove(category);
            //             ModelState.Clear();
            //         }
            //         return View(note);
            // }

            if (ModelState.IsValid)
            {
                using(var context = new DBContext()){
                    try {
                        context.Update(note);
                        await context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    } catch(DbUpdateException e) {
                        ModelState.AddModelError("Title", e.Message);
                        return View(note);
                    }
                }
            }
            return View(note);
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
