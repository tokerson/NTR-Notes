using System;
using System.ComponentModel.DataAnnotations;

namespace Z01.Models 
{
       public class Note 
    {
        public Note(string title, string[] categories, DateTime date, string content = "", string extenstion = "txt") {
            this.title = title;
            this.categories = categories;
            this.date = date;
            this.content = content;
            this.extenstion = extenstion;
        }
        [Required(ErrorMessage = "Title is required")]
        public string title { get; set;}
        public string[] categories { get; set;}
        [DataType(DataType.Date)]
        public DateTime date {get; set;}
        public string content {get; set;}
        
        public string extenstion {get; set;}
    }
}
