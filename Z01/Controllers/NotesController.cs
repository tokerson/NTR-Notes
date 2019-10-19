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
        public IActionResult Index()
        {
            NoteRepository repository = new NoteRepository();
            ViewData["Categories"] = allCategories;
            List<Note> notes = (List<Note>) repository.FindAll();
            return View(notes);
        }

        public IActionResult Edit(string title)
        {
            NoteRepository repository = new NoteRepository();
            Note note = repository.FindById(title);
            return View(note);
        }

        // [HttpPost]
        // public IActionResult Edit(string note) 
        // {
        //     return View(loadNote(note));
        // }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        

    

        
    }
}
