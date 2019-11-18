using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Z02
{
    [Table("Note", Schema = "tokarzewski")]
    public class Note
    {
        [Key]
        public int NoteID { get; set; }
        [Required]
        public DateTime NoteDate { get; set; }
        [Required]
        [MaxLength(64)]
        public string Title { get; set; }
        public string Description { get; set; }
        // public ICollection<NoteCategory> NoteCategories { get; set; }
    }
}