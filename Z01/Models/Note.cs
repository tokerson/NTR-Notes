using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Z01.Models
{
    public class Note
    {
        public Note()
        {
            this.extension = "txt";
            this.date = DateTime.Now;
            this.categories = new string[]{};
        }
        public Note(string title, string[] categories, DateTime date, string content = "", string extenstion = "txt")
        {
            this.title = title;
            this.categories = categories;
            this.date = date.Date;
            this.content = content;
            this.extension = extenstion;
        }
        [Required(ErrorMessage = "Title is required")]
        public string title { get; set; }
        public string[] categories { get; set; }
        [DataType(DataType.Date)]
        public DateTime date { get; set; }
        public string content { get; set; }

        [AvailableExtension, Required(ErrorMessage = "Extension must be either txt or md")]
        public string extension { get; set; }
    }

    public class AvailableExtension : ValidationAttribute
    {
        // private readonly string _extension;
        public readonly string[] availableExtensions = new string[] { "txt", "md" };
        // public AvailableExtension(string extension)
        // {
            // this._extension = extension;
        // }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!availableExtensions.Contains(value))
            {
                return new ValidationResult(FormatErrorMessage("Extension must be either txt or md"));
            }
            return ValidationResult.Success;
        }
    }
}
