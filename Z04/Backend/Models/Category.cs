using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class Category
    {
        public Category()
        {
            NoteCategory = new HashSet<NoteCategory>();
        }

        public int Idcategory { get; set; }
        public string Name { get; set; }

        public virtual ICollection<NoteCategory> NoteCategory { get; set; }
    }
}
