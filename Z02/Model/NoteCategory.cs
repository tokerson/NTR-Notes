using System.ComponentModel.DataAnnotations.Schema;

namespace Z02.Model
{
    [Table("NoteCategory", Schema = "tokarzewski")]
    public class NoteCategory
    {
        public int NoteID { get; set; }
        public Note Note { get; set; }

        public int CategoryID { get; set; }
        public Category Category { get; set; }
    }
}