using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Z02
{
    [Table("Category", Schema = "tokarzewski")]
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }
        [Required]
        [MaxLength(64)]
        public string Title { get; set; }
        // public ICollection<NoteCategory> NoteCategories { get; set; }
    }
}