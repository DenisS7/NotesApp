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
            SortedSet<int> IDs = new SortedSet<int>();
            foreach (Note note in Notes)
            {
                IDs.Add(note.ID);
            }

            int newID = 0;
            while (IDs.Contains(newID))
            {
                newID++;
            }
            return newID;
        }

        public Note CreateNote()
        {
            Note note = new Note(GetNewNoteID(), DateTime.Now);
            Notes.Add(note);
            return note;
        }
    }
}
