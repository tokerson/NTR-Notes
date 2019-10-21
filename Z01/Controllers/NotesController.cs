using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Z01.Models;
using Z01.Repositories;
using System.Collections;

namespace Z01.Controllers
{
    public class NotesController : Controller
    {
        List<string> allCategories = new List<string>();
        List<Note> notes;
        public IActionResult Index()
        {
            NoteRepository repository = new NoteRepository();
            ViewData["Categories"] = allCategories;
            notes = (List<Note>) repository.FindAll();
            return View(notes);
        }

        public IActionResult Edit(string title)
        {
            NoteRepository repository = new NoteRepository();
            Note note = repository.FindById(title);
            if ( note == null ) 
            {
                return NotFound();
            }
            return View(note);
        }

        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string old_title, [Bind("title, categories, date, content, extension")] Note note) 
        {
            NoteRepository noteRepository = new NoteRepository();
            Note oldNote = noteRepository.FindById(old_title);

            if (oldNote == null) {
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
