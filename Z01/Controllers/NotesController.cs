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
        public IActionResult Index(DateTime start_date, DateTime last_date, string chosenCategory = "")
        {
            // Console.WriteLine("Start date " + start_date);
            // Console.WriteLine(start_date == default(DateTime));
            // Console.WriteLine("Default " + default(DateTime));
            // Console.WriteLine("Last date " + last_date);
            Console.WriteLine("Category " + chosenCategory);
            NoteRepository repository = new NoteRepository();
            
            notes = (List<Note>) repository.FindAll();
            
            foreach( Note note in notes) 
            {
                foreach( string category in note.categories )
                {
                    allCategories.Add(category);
                }
            }

            if( chosenCategory != null && chosenCategory != "")
            {
                notes = notes.Where(note => note.categories.Contains(chosenCategory)).ToList();
            }
            
            ViewData["StartDate"] = start_date;
            ViewData["LastDate"] = last_date;
            ViewData["Category"] = chosenCategory;
            ViewData["Categories"] = allCategories;
            return View(notes);
        }

        // public IActionResult Index(DateTime start_date, DateTime last_date)
        // {
        //     Console.WriteLine("Start date " + start_date);
        //     Console.WriteLine("Last date " + last_date);
        //     return View(notes);
        // }

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

        // public IActionResult New()
        // {
        //     return View();
        // }

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
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string old_title, [Bind("title, categories, date, content, extension")] Note note) 
        {
            NoteRepository noteRepository = new NoteRepository();
            Note oldNote = noteRepository.FindById(old_title);

            Console.WriteLine("Old title " + old_title);
            Console.WriteLine("Title " + note.title);

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
