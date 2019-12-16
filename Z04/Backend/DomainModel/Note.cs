using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend.DomainModel
{
    public partial class Note
    {
        public int Idnote { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public short IsMarkdown { get; set; }
        public DateTime Date { get; set; }
        public byte[] Timestamp { get; set; }

        public virtual ICollection<String> Categories { get; set; }

        public static Note Create(Models.Note note) {
            return new Note() {
                Idnote=note.Idnote,
                Title=note.Title,
                Date=note.Date,
                Description=note.Description,
                IsMarkdown=note.IsMarkdown,
                Timestamp=note.Timestamp,
                Categories=note.NoteCategory.Select(nc => nc.IdcategoryNavigation.Name).ToList(),
            };
        }
    }
}
