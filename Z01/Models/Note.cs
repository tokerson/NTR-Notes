using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Z01.Models
{
    public class Note
    {
        public Note()
        {
            this.title = "";
            this.date = DateTime.Now;
            this.categories = new List<string>();
        }
        public Note(string title, List<string> categories, DateTime date, string content = "")
        {
            this.title = title;
            this.categories = categories;
            this.date = date.Date;
            this.content = content;
        }
        [Required(ErrorMessage = "Title is required")]
        public string title { get; set; }
        public List<string> categories { get; set; }
        [DataType(DataType.Date)]
        public DateTime date { get; set; }
        public string content { get; set; }

        public bool markdown { get; set; }
    }
}
