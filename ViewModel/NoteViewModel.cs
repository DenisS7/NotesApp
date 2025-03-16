using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NotesApp.ViewModel
{
    public class NoteViewModel : ViewModelBase
    {
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

        public NoteViewModel()
        {
            
        }
	}
}
