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
        private bool isShortNoteMenuOpen;
        public Note Note => note;
        public int NoteID => note.ID;
        public string NoteText => note.Text;
        public DateTime LastUpdateDate => note.LastUpdatedDate;

        public bool IsShortNoteMenuOpen
        {
            get => isShortNoteMenuOpen;
            set { isShortNoteMenuOpen = value; OnPropertyChanged(nameof(IsShortNoteMenuOpen)); }
        }


        public ICommand OpenNoteCommand { get; }
        public ICommand DeleteNoteCommand { get; }
        public ICommand OpenShortNoteMenuCommand { get; }
        public ShortNoteViewModel(Note note, NavigationService navigationService)
        {
            isShortNoteMenuOpen = false;
            this.note = note;
            this.navigationService = navigationService;
            OpenNoteCommand = new RelayCommand(param => OpenNoteWindow());
            DeleteNoteCommand = new DeleteNoteCommand(navigationService);
            NoteUpdateMessageBus.NoteUpdated += OnNoteUpdated;

            OpenShortNoteMenuCommand = new RelayCommand(_ =>
            {
                IsShortNoteMenuOpen = true;
            });
        }

        private void OnNoteUpdated(Note note)
        {
            if (this.note.ID == note.ID)
            {
                OnPropertyChanged(nameof(LastUpdateDate));
                OnPropertyChanged(nameof(NoteText));
            }
        }

        private void OpenNoteWindow()
        {
            navigationService.OpenNote(note);
        }
    }
}
