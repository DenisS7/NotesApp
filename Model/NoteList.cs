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
        public NoteList()
        {
            Notes = new ObservableCollection<Note>();
        }

        public IEnumerable<Note> GetAllNotes()
        {
            return Notes;
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
