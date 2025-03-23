using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotesApp.Message;
using NotesApp.Model;

namespace NotesApp.ViewModel
{
    public class ShortNoteViewModel : ViewModelBase
    {
        private Note note { get; }
        public string noteText => note.Text;
        public DateTime lastUpdateDate => note.LastUpdatedDate;

        public ShortNoteViewModel(Note note)
        {
            this.note = note;
            NoteUpdateMessageBus.NoteUpdated += OnNoteUpdated;
        }

        private void OnNoteUpdated(Note note)
        {
            if (this.note.ID == note.ID)
            {
                OnPropertyChanged(nameof(noteText));
            }
        }
    }
}
