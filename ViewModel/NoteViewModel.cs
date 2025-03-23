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
    public class NoteViewModel : ViewModelBase
    {
        private readonly INavigationService navigationService;
        private Note note;
        private bool isMenuVisible;

        public bool IsMenuVisible
        {
            get
            {
                return isMenuVisible;
            }
            set
            {
                isMenuVisible = value;
                OnPropertyChanged(nameof(IsMenuVisible));
            }
        }

		private string noteText;
		public int NoteId { get; }
		public string NoteText
		{
			get
			{
				return noteText;
			}
			set
			{
				noteText = value;
                note.Text = noteText;
				OnPropertyChanged(nameof(NoteText));
				NoteUpdateMessageBus.MessageNoteUpdated(note);
			}
		}

		public ICommand CreateNoteCommand { get; }
		public ICommand CloseCommand { get; }
		public ICommand OpenNoteMenuCommand { get; }
        public ICommand CloseNoteMenuCommand { get; }
        public ICommand OpenNoteListCommand { get; }

        public NoteViewModel(NavigationService navigationService, Note note)
        {
            this.navigationService = navigationService;
            this.note = note;
            isMenuVisible = false;
            noteText = note.Text;
            NoteId = note.ID;
            OpenNoteListCommand = new OpenNoteListCommand(navigationService);
            CreateNoteCommand = new CreateNoteCommand(navigationService);
            CloseCommand = new CloseCommand(navigationService);
            OpenNoteMenuCommand = new RelayCommand(param => ShowMenu());
            CloseNoteMenuCommand = new RelayCommand(param => CloseMenu());
        }

        private void ShowMenu()
        {
            IsMenuVisible = true;
        }

        private void CloseMenu()
        {
            IsMenuVisible = false;
        }
    }
}
