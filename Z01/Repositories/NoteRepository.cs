using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Z01.Models;

namespace Z01.Repositories
{
    public class NoteRepository : IRepository<Note, string>
    {
        private const string directory = "./data";
        public Note FindById(string title)
        {
            Note note = null;
            
            using (StreamReader file = new StreamReader("./data/"+title+".txt"))
            {
                string line = file.ReadLine();
                List<string> categories = extractCategories(line);
                line = file.ReadLine();
                DateTime date = extractDate(line);
                string content = "";
                while ((line = file.ReadLine()) != null)
                {
                    content += line;
                }

                note = new Note(title, categories.ToArray(), date, content);
            }

            return note;
        }

        public IEnumerable FindAll()
        {
            string[] files = Directory.GetFiles(directory);
            List<Note> notes = new List<Note>(); 
            foreach (string fileName in files) 
            {
                using (StreamReader file = new StreamReader(fileName))
                {
                    //reading line describing category
                    string line = file.ReadLine();
                    List<string> categories = extractCategories(line);
                    // foreach(string category in categories)
                    // {
                    //     if (!allCategories.Contains(category))
                    //     {
                    //         allCategories.Add(category);
                    //     }
                    // }
                    //reading line describing date
                    line = file.ReadLine();
                    DateTime date = extractDate(line);

                    Note newNote = new Note(extractNoteTitle(fileName), categories.ToArray(), date);
                    notes.Add(newNote);
                }
            }

            return notes;
        }

        public void Update(Note oldNote, Note newNote)
        {
            //delete old 
            //save new
        }

        public void Save(Note note)
        {
            //create new file
        }

        public void Delete(string title)
        {
            string[] files = Directory.GetFiles(directory);
            string fileToDelete = files.Single(file => extractNoteTitle(file).Equals(title));
            File.Delete(fileToDelete);
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