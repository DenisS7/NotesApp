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

        public NoteViewModel(NavigationService navigationService, Note note)
        {
            this.navigationService = navigationService;
            this.note = note;
            noteText = note.Text;
            NoteId = note.ID;
            CreateNoteCommand = new CreateNoteCommand(navigationService);
            CloseCommand = new CloseCommand(navigationService);
            OpenNoteMenuCommand = new OpenNoteMenuCommand(navigationService);
        }
	}
}
