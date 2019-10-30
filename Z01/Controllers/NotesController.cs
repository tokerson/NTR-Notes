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
        HashSet<string> allCategories = new HashSet<string>();
        List<Note> notes;

        public IActionResult Index(DateTime start_date, DateTime last_date, int? pageNumber, string chosenCategory = "")
        {
            NoteRepository repository = new NoteRepository();
            int pageSize = 1;

            notes = (List<Note>)repository.FindAll();

            foreach (Note note in notes)
            {
                foreach (string category in note.categories)
                {
                    allCategories.Add(category);
                }
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

            if (chosenCategory != null && chosenCategory != "")
            {
                notes = notes.Where(note => note.categories.Contains(chosenCategory)).ToList();
            }

            TempData["chosenCategory"] = chosenCategory;
            TempData.Keep("chosenCategory");
            TempData.Keep("startDate");
            TempData.Keep("lastDate");

            ViewData["Categories"] = allCategories;

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

        public IActionResult New([Bind("title, categories, date, content, extension")] Note note)
        {
            if (ModelState.IsValid)
            {
                NoteRepository noteRepository = new NoteRepository();
                noteRepository.Save(note);
                return RedirectToAction(nameof(Index));
            }
            return View(note);
        }

        [HttpPost]
        // [ValidateAntiForgeryToken]
        public IActionResult Edit(string old_title, [Bind("title, categories, date, content, extension")] Note note)
        {
            NoteRepository noteRepository = new NoteRepository();
            Note oldNote = noteRepository.FindById(old_title);

            Console.WriteLine("Old title " + old_title);
            Console.WriteLine("Title " + note.title);

            if (oldNote == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                noteRepository.Update(oldNote, note);
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
