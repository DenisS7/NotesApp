using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using NotesApp.Commands;
using NotesApp.Message;
using NotesApp.Model;
using NotesApp.Services;

namespace NotesApp.ViewModel
{
    public class NoteListViewModel : ViewModelBase
    {
        private NavigationService navigationService;
        private NoteList noteList;
        private string searchText;
        public string SearchText
        {
            get
            {
                return searchText;
            }
            set
            {
                searchText = value;
                OnPropertyChanged(nameof(SearchText));
                DisplayedShortNoteViewModels.Refresh();
            }
        }

        public ObservableCollection<ShortNoteViewModel> ShortNoteViewModels { get; }

        public ICollectionView DisplayedShortNoteViewModels { get; }
        public ICommand CloseCommand { get; }
        public ICommand CreateNoteCommand { get; }
        public ICommand ClearSearchCommand { get; }

        public NoteListViewModel(NavigationService navigationService, NoteList noteList)
        {
            this.navigationService = navigationService;
            this.noteList = noteList;
            navigationService.NoteCreated += OnNoteCreated;
            navigationService.NoteDeleted += OnNoteDeleted;
            CloseCommand = new CloseCommand(navigationService);
            ShortNoteViewModels =
                new ObservableCollection<ShortNoteViewModel>(
                    noteList.Notes.Select(note => new ShortNoteViewModel(note, navigationService)));
            DisplayedShortNoteViewModels = CollectionViewSource.GetDefaultView(ShortNoteViewModels);
            DisplayedShortNoteViewModels.Filter = FilterDisplayedNotes;
            CreateNoteCommand = new CreateNoteCommand(navigationService);
            ClearSearchCommand = new RelayCommand(ClearSearch);
        }

        private void OnNoteCreated(object sender, Note newNote)
        {
            ShortNoteViewModels.Add(new ShortNoteViewModel(newNote, navigationService));
        }

        private void OnNoteDeleted(object sender, Note deletedNote)
        {
            foreach (var shortNoteViewModel in ShortNoteViewModels)
            {
                if (shortNoteViewModel.NoteID == deletedNote.ID)
                {
                    ShortNoteViewModels.Remove(shortNoteViewModel);
                    break;
                }
            }
        }

        private void ClearSearch(object? obj)
        {
            SearchText = string.Empty;
        }

        private bool FilterDisplayedNotes(object obj)
        {
            if (obj is ShortNoteViewModel shortNoteViewModel)
            {
                return shortNoteViewModel.Note.ContainsText(SearchText);
            }

            return false;
        }
    }
}
