using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Z01.Models;
using Z01.Repositories;

namespace Z01.Controllers
{
    public class NotesController : Controller
    {
        private HashSet<string> allCategories = new HashSet<string>(){"All"};
        private List<Note> notes;

        public IActionResult Index(DateTime start_date, DateTime last_date, int? pageNumber, string btnSubmit, string chosenCategory = "All")
        {
            NoteRepository repository = new NoteRepository();
            int pageSize = 3;

            notes = (List<Note>)repository.FindAll();


            foreach (Note note in notes)
            {
                foreach (string category in note.categories)
                {
                    allCategories.Add(category);
                }
            }

            ViewData["Categories"] = allCategories;

            if (btnSubmit == "Clear")
            {
                TempData.Clear();
                return View(new PaginatedList<Note>(notes, pageNumber ?? 1, pageSize));
            }

            if (last_date == DateTime.MinValue)
            {
                last_date = DateTime.MaxValue;
                TempData["lastDate"] = last_date;
            }
            else
            {
                TempData["lastDate"] = last_date.ToString("yyyy-MM-dd");
            }

            if (start_date != DateTime.MinValue)
            {
                TempData["startDate"] = start_date.ToString("yyyy-MM-dd");
            }

            notes = notes.Where(note => note.date >= start_date && note.date <= last_date).ToList();

            if (chosenCategory != null && chosenCategory != "All")
            {
                notes = notes.Where(note => note.categories.Contains(chosenCategory)).ToList();
            }

            TempData["chosenCategory"] = chosenCategory;
            TempData.Peek("chosenCategory");
            TempData.Peek("startDate");
            TempData.Peek("lastDate");

            return View(new PaginatedList<Note>(notes, pageNumber ?? 1, pageSize));
        }

        public IActionResult Edit(string title)
        {
            NoteRepository repository = new NoteRepository();
            Note note = repository.FindById(title);

            if (note == null)
            {
                return NotFound();
            }

            return View(note);
        }

        public IActionResult New(Note note, string category = "", string btnSubmit = "")
        {
            category = category ?? "";
            category = category.Trim();

            switch (btnSubmit)
            {
                case "Add":
                    if (category.Length > 0)
                    {
                        note.categories.Add(category);
                        ModelState.Clear();
                    }
                    return View(note);
                case "Remove":
                    if (note.categories.Contains(category))
                    {
                        note.categories.Remove(category);
                        ModelState.Clear();
                    }
                    return View(note);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    NoteRepository noteRepository = new NoteRepository();
                    noteRepository.Save(note);
                }
                catch (Z01.Repositories.DuplicatedNoteTitleException e)
                {
                    ModelState.AddModelError("title", e.Message);
                    return View(note);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(note);
        }

        [HttpPost]
        // [ValidateAntiForgeryToken]
        public IActionResult Edit(string old_title, Note note, string category = "", string btnSubmit = "")
        {
            NoteRepository noteRepository = new NoteRepository();
            Note oldNote = noteRepository.FindById(old_title);

            if (oldNote == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    noteRepository.Update(oldNote, note);
                }
                catch (Z01.Repositories.DuplicatedNoteTitleException e)
                {
                    ModelState.AddModelError("title", e.Message);
                    return View(note);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(note);
        }

        public IActionResult Delete(string title)
        {
            NoteRepository noteRepository = new NoteRepository();
            noteRepository.Delete(title);

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
