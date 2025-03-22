using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NotesApp.Commands;
using NotesApp.Model;
using NotesApp.Services;

namespace NotesApp.ViewModel
{
    public class NoteViewModel : ViewModelBase
    {
        private readonly INavigationService navigationService;
        private Note note;
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
				OnPropertyChanged(nameof(NoteText));
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
            CreateNoteCommand = new CreateNoteCommand(navigationService);
            CloseCommand = new CloseCommand(navigationService);
        }
	}
}
