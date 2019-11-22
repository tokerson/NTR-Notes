using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Z02.Model
{
    [Table("Note", Schema = "tokarzewski")]
    public class Note
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NoteID { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime NoteDate { get; set; }
        [Required]
        [MaxLength(64)]
        public string Title { get; set; }
        public string Description { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public ICollection<NoteCategory> NoteCategories { get; set; }
    }
}