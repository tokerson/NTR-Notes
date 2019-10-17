using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Z01.Models;
using System.Collections;

namespace Z01.Controllers
{
    public class NotesController : Controller
    {
        public IActionResult Index()
        {
            // Note[] notes = new Note[]{
            //     new Note("Results of Suzuka Quali",new string[]{"Formula 1"}, DateTime.Now),
            //     new Note("Books to read in 2010",["Books"], DateTime.Now),
            //     new Note("Books to read in 2009",["Books"], DateTime.Now),
            // };
            List<Note> notes = loadNotes();
            return View(notes);
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private List<Note> loadNotes() 
        {
            string directory = "./data";
            string[] files = Directory.GetFiles(directory);
            List<Note> notes = new List<Note>(); 
            foreach (string fileName in files) 
            {
                using (StreamReader file = new StreamReader(fileName))
                {
                    //reading line describing category
                    string line = file.ReadLine();
                    List<string> categories = extractCategories(line);
                    //reading line describing date
                    line = file.ReadLine();
                    DateTime date = extractDate(line);

                    Note newNote = new Note(extractNoteTitle(fileName), categories.ToArray(), date);
                    notes.Add(newNote);
                }
            }

            return notes;
        }

        private List<string> extractCategories(string categoryString) 
        {
            return categoryString.Split(':')[1].Split(',').Select(item => item.Trim()).ToList();
        }

        private DateTime extractDate(string dateString) 
        {
            string date = dateString.Split(':')[1];
            date = date.Trim();
             
            return Convert.ToDateTime(date);
        }

        private string extractNoteTitle(string fileName) 
        {
            return fileName.Split('/').Last().Split('.')[0];
        }
    }
}
