using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Z01.Models;

namespace Z01.Repositories
{
    public class NoteRepository : IRepository<Note, string>
    {
        private const string directory = "./data";
        public Note FindById(string title)
        {
            Note note = null;

            using (StreamReader file = new StreamReader("./data/" + title + ".txt"))
            {
                string line = file.ReadLine();
                HashSet<string> categories = extractCategories(line);
                line = file.ReadLine();
                DateTime date = extractDate(line);
                string content = "";
                while ((line = file.ReadLine()) != null)
                {
                    content += line;
                }

                note = new Note(title, categories.ToList(), date, content);
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
                    HashSet<string> categories = extractCategories(line);
                    line = file.ReadLine();
                    DateTime date = extractDate(line);

                    Note newNote = new Note(extractNoteTitle(fileName), categories.ToList(), date);
                    notes.Add(newNote);
                }
            }

            notes.Sort((note1, note2) => note1.title.CompareTo(note2.title));

            return notes;
        }

        public void Update(Note oldNote, Note newNote)
        {
            Delete(oldNote.title);
            Save(newNote);
        }

        public void Save(Note note)
        {
            StringBuilder stringBuilder = new StringBuilder("");
            stringBuilder.Append("category: ");
            for (int i = 0; i < note.categories.Count(); ++i)
            {
                stringBuilder.Append(note.categories[i]);
                if (i < note.categories.Count() - 1)
                {
                    stringBuilder.Append(",");
                }
            }
            stringBuilder.Append("\ndate: ");
            stringBuilder.Append(note.date.ToString("yyyy/MM/dd") + "\n");
            stringBuilder.Append(note.content);
            string path = directory + "/" + note.title + "." + note.extension;
            File.WriteAllText(path, stringBuilder.ToString());
        }

        public void Delete(string title)
        {
            string[] files = Directory.GetFiles(directory);
            string fileToDelete = files.Single(file => extractNoteTitle(file).Equals(title));
            File.Delete(fileToDelete);
        }
        private HashSet<string> extractCategories(string categoryString)
        {
            HashSet<string> categories = categoryString.Split(':')[1].Split(',').Select(item => item.Trim()).ToHashSet();
            categories.Remove("");
            return categories;
        }

        private DateTime extractDate(string dateString)
        {
            string date = dateString.Split(':')[1];
            date = date.Trim();

            //TODO:Add handling exceptions 
            return Convert.ToDateTime(date);
        }

        private string extractNoteTitle(string fileName)
        {
            return fileName.Split('/').Last().Split('.')[0];
        }

    }
}