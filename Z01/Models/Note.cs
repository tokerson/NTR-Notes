using System;
using System.ComponentModel.DataAnnotations;

namespace Z01.Models 
{
       public class Note 
    {
        public Note(string title, string[] categories, DateTime date) {
            this.title = title;
            this.categories = categories;
            this.date = date;
        }
        public int Id { get;}
        public string title { get; set;}
        public string[] categories { get; set;}
        [DataType(DataType.Date)]
        public DateTime date {get; set;}
    }
}
