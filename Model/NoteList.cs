using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp.Model
{
    public class NoteList
    {
        public List<Note> Notes { get; set; }
        public List<Tag> Tags { get; set; }

        public NoteList()
        {
            Notes = new List<Note>();
            Tags = new List<Tag>();
        }

        public IEnumerable<Note> GetAllNotes()
        {
            return Notes;
        }

        public IEnumerable<Tag> GetAllTags()
        {
            return Tags;
        }

        public IEnumerable<Note> GetNotesWithTags(List<Tag> tags)
        {
            List<Note> notesWithTags = new List<Note>();
            foreach (var note in Notes)
            {
                if(note.HasTags(tags))
                    notesWithTags.Add(note);
            }

            return notesWithTags;
        }
    }
}
