using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using NotesApp.Model;
using NotesApp.View;
using NotesApp.ViewModel;

namespace NotesApp.Services
{
    public class NavigationService : INavigationService
    {
        private readonly NoteList noteList;
        private readonly Dictionary<ViewModelBase, Window> openWindows = new Dictionary<ViewModelBase, Window>();

        public NavigationService(NoteList noteList)
        {
            this.noteList = noteList;
        }

        public void CreateNote()
        {
            Note note = noteList.CreateNote();
            NoteViewModel noteViewModel = new NoteViewModel(this, note);
            OpenNote(noteViewModel);
        }
        public void OpenNote(NoteViewModel noteViewModel)
        {
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

        public void OpenMenu()
        {
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
