using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using NotesApp.Model;
using NotesApp.Saving;
using NotesApp.View;
using NotesApp.ViewModel;

namespace NotesApp.Services
{
    public class NavigationService : INavigationService
    {
        public event EventHandler<Note> NoteCreated;
        public event EventHandler<Note> NoteDeleted;
        private readonly NoteList noteList;
        private readonly Dictionary<ViewModelBase, Window> openWindows = new Dictionary<ViewModelBase, Window>();

        public NavigationService(NoteList noteList)
        {
            this.noteList = noteList;
        }

        public void SaveNoteList()
        {
            NoteListSavingUtility.SaveNoteList(noteList);
        }

        public void CreateNote()
        {
            Note note = noteList.CreateNote();
            NoteCreated?.Invoke(this, note);
            OpenNote(note);
            SaveNoteList();
        }

        public void DeleteNote(Note note)
        {
            if (noteList.Notes.Contains(note))
            {
                foreach (var window in openWindows)
                {
                    if (window.Key is NoteViewModel noteViewModel)
                    {
                        if(noteViewModel.NoteId == note.ID)
                            CloseWindow(noteViewModel);
                    }
                }
                noteList.Notes.Remove(note);
                NoteDeleted.Invoke(this, note);
            }
            SaveNoteList();
        }
        public void OpenNote(Note note)
        {
            foreach (var window in openWindows)
            {
                if (window.Key is NoteViewModel currentNoteViewModel)
                {
                    if (currentNoteViewModel.NoteId == note.ID)
                    {
                        window.Value.Activate();
                        return;
                    }
                }
            }
            NoteViewModel noteViewModel = new NoteViewModel(this, note);
            NoteView noteWindow = new NoteView();
            noteWindow.DataContext = noteViewModel;

            openWindows.Add(noteViewModel, noteWindow);

            noteWindow.Closed += (sender, args) =>
            {
                if (openWindows.ContainsKey(noteViewModel))
                {
                    openWindows.Remove(noteViewModel);
                }
            };

            noteWindow.Show();
        }

        public void OpenNoteList()
        {
            foreach (var window in openWindows)
            {
                if (window.Key is NoteListViewModel)
                {
                    window.Value.Activate();
                    return;
                }
            }
            NoteListViewModel noteListViewModel = new NoteListViewModel(this, noteList);
            NoteListView noteListWindow = new NoteListView();
            noteListWindow.DataContext = noteListViewModel;

            openWindows.Add(noteListViewModel, noteListWindow);

            noteListWindow.Closed += (sender, args) =>
            {
                if (openWindows.ContainsKey(noteListViewModel))
                {
                    openWindows.Remove(noteListViewModel);
                }
            };

            noteListWindow.Show();
        }

        public void CloseWindow(ViewModelBase viewModelBase)
        {
            if (openWindows.TryGetValue(viewModelBase, out Window window))
            {
                window.Close();
            }
        }
    }
}
