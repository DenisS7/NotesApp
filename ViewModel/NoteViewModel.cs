using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
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
        private readonly DispatcherTimer autoSaveTimer;
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

        public int NoteId
        {
            get
            {
                return note.ID;
            }
        }
		public string NoteText
		{
			get
            {
                return note.Text;
            }
			set
            {
                note.Text = value;
                note.LastUpdatedDate = DateTime.Now;
                OnPropertyChanged(nameof(NoteText));
                RestartAutoSaveTimer();
                NoteUpdateMessageBus.MessageNoteUpdated(note);
			}
		}

        public Color Color
        {
            get
            {
                return note.Color;
            }
            set
            {
                note.Color = value;
                OnPropertyChanged(nameof(Color));
                navigationService.SaveNoteList();
                NoteUpdateMessageBus.MessageNoteUpdated(note);
            }
        }

		public ICommand CreateNoteCommand { get; }
        public ICommand DeleteNoteCommand { get; }
		public ICommand CloseCommand { get; }
		public ICommand OpenNoteMenuCommand { get; }
        public ICommand CloseNoteMenuCommand { get; }
        public ICommand OpenNoteListCommand { get; }
        public ICommand SetNoteColorCommand { get; }

        public NoteViewModel(NavigationService navigationService, Note note)
        {
            this.navigationService = navigationService;
            this.note = note;
            isMenuVisible = false;
            OpenNoteListCommand = new OpenNoteListCommand(navigationService);
            CreateNoteCommand = new CreateNoteCommand(navigationService);
            DeleteNoteCommand = new DeleteNoteCommand(navigationService);
            CloseCommand = new CloseCommand(navigationService);
            OpenNoteMenuCommand = new RelayCommand(param => ShowMenu());
            CloseNoteMenuCommand = new RelayCommand(param => CloseMenu());
            SetNoteColorCommand = new RelayCommand(SetNoteColor);
            autoSaveTimer = new DispatcherTimer{ Interval = TimeSpan.FromSeconds(1) };
            autoSaveTimer.Tick += AutoSaveTimerTick;
        }

        private void ShowMenu()
        {
            IsMenuVisible = true;
        }

        private void CloseMenu()
        {
            IsMenuVisible = false;
        }

        private void RestartAutoSaveTimer()
        {
            autoSaveTimer.Stop();
            autoSaveTimer.Start();
        }

        private void AutoSaveTimerTick(object sender, EventArgs e)
        {
            autoSaveTimer.Stop();
            navigationService.SaveNoteList();
        }

        private void SetNoteColor(object? parameter)
        {
            if (parameter is Rectangle rect && rect.Fill is SolidColorBrush scb)
            {
                Color = scb.Color;
                IsMenuVisible = false;
                
            }
        }
    }
}
