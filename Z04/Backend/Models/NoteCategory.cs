using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class NoteCategory
    {
        public int Idnote { get; set; }
        public int Idcategory { get; set; }

        public virtual Category IdcategoryNavigation { get; set; }
        public virtual Note IdnoteNavigation { get; set; }
    }
}
