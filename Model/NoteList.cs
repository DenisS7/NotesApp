using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp.Model
{
    public class NoteList
    {
        public ObservableCollection<Note> Notes { get; set; }
        public List<Tag> Tags { get; set; }

        public NoteList()
        {
            Notes = new ObservableCollection<Note>();
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

        private int GetNewNoteID()
        {
            return Notes.Count();
        }

        public Note CreateNote()
        {
            Note note = new Note(GetNewNoteID(), DateTime.Now);
            note.Text = "Example Text Example Text Example Text Example Text Example Text Example Text\n" +
                        "Example Text Example Text Example Text Example Text Example Text Example Text\n" +
                        "Example Text Example Text Example Text Example Text Example Text Example Text\n" +
                        "Example Text Example Text Example Text Example Text Example Text Example Text\n";
            Notes.Add(note);
            return note;
        }
    }
}
