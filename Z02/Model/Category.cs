using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Z02.Model
{
    [Table("Category", Schema = "tokarzewski")]
    public class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryID { get; set; }
        [Required]
        [StringLength(64)]        
        public string Title { get; set; }
        public ICollection<NoteCategory> NoteCategories { get; set; }
    }
}