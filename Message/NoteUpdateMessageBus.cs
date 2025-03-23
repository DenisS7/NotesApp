using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotesApp.Model;

namespace NotesApp.Message
{
    public static class NoteUpdateMessageBus
    {
        public static event Action<Note> NoteUpdated;

        public static void MessageNoteUpdated(Note note)
        {
            NoteUpdated?.Invoke(note);
        }
    }
}
