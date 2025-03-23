using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NotesApp.Commands;
using NotesApp.Model;
using NotesApp.Services;

namespace NotesApp.ViewModel
{
    public class NoteListViewModel : ViewModelBase
    {
        private NavigationService navigationService;
        private NoteList noteList;

        public ObservableCollection<ShortNoteViewModel> ShortNoteViewModels { get; private set; }

        public ICommand CloseCommand { get; }
        public ICommand CreateNoteCommand { get; }

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
            CreateNoteCommand = new CreateNoteCommand(navigationService);
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
    }
}
