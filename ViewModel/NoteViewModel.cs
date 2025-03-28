using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
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

        private bool isBoldActive = false;
        public bool IsBoldActive
        {
            get
            {
                return isBoldActive;
            }
            set
            {
                isBoldActive = value;
                OnPropertyChanged(nameof(IsBoldActive));
            }
        }

        private bool isItalicActive = false;
        public bool IsItalicActive
        {
            get
            {
                return isItalicActive;
            }
            set
            {
                isItalicActive = value;
                OnPropertyChanged(nameof(IsItalicActive));
            }
        }

        private bool isUnderlineActive = false;
        public bool IsUnderlineActive
        {
            get
            {
                return isUnderlineActive;
            }
            set
            {
                isUnderlineActive = value;
                OnPropertyChanged(nameof(IsUnderlineActive));
            }
        }
        public Note Note => note;

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

		public int NoteId { get; }
		private string noteText;
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
                note.LastUpdatedDate = DateTime.Now;
                OnPropertyChanged(nameof(NoteText));
				NoteUpdateMessageBus.MessageNoteUpdated(note);
			}
		}

		public ICommand CreateNoteCommand { get; }
        public ICommand DeleteNoteCommand { get; }
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
            DeleteNoteCommand = new DeleteNoteCommand(navigationService);
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
