using System;
using System.Collections.Generic;
using Backend.DomainModel;

namespace Backend.DomainModel
{
    public partial class Category
    {
        public int Idcategory { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Note> Notes { get; set; }
    }
}
