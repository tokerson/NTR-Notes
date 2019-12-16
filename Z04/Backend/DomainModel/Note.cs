using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend.DomainModel
{
    public partial class Note
    {
        public Note(Models.Note note) {
            this.Idnote=note.Idnote;
            this.Title=note.Title;
            this.Date=note.Date;
            this.Description=note.Description;
            this.IsMarkdown=note.IsMarkdown;
            this.Timestamp=note.Timestamp;
            this.Categories=note.NoteCategory.Select(nc => nc.IdcategoryNavigation.Name).ToList();
        }
        public int Idnote { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public short IsMarkdown { get; set; }
        public DateTime Date { get; set; }
        public byte[] Timestamp { get; set; }

        public virtual ICollection<String> Categories { get; set; }
    }
}
