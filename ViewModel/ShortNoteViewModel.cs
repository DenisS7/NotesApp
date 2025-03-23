using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NotesApp.Commands;
using NotesApp.Message;
using NotesApp.Model;
using NotesApp.Services;

namespace NotesApp.ViewModel
{
    public class ShortNoteViewModel : ViewModelBase
    {
        private Note note { get; }
        private NavigationService navigationService;
        public int NoteID => note.ID;
        public string NoteText => note.Text;
        public DateTime LastUpdateDate => note.LastUpdatedDate;

        public ICommand OpenNoteCommand { get; }
        public ShortNoteViewModel(Note note, NavigationService navigationService)
        {
            this.note = note;
            this.navigationService = navigationService;
            OpenNoteCommand = new RelayCommand(param => OpenNoteWindow());
            NoteUpdateMessageBus.NoteUpdated += OnNoteUpdated;
        }

        private void OnNoteUpdated(Note note)
        {
            if (this.note.ID == note.ID)
            {
                OnPropertyChanged(nameof(NoteText));
            }
        }

        private void OpenNoteWindow()
        {
            navigationService.OpenNote(note);
        }
    }
}
