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
        private readonly Dictionary<NoteViewModel, Window> openNotes = new Dictionary<NoteViewModel, Window>();

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

            openNotes.Add(noteViewModel, noteWindow);

            noteWindow.Closed += (sender, args) =>
            {
                if (openNotes.ContainsKey(noteViewModel))
                {
                    openNotes.Remove(noteViewModel);
                }
            };

            noteWindow.Show();
        }

        public void CloseNote(NoteViewModel noteViewModel)
        {
            if (openNotes.TryGetValue(noteViewModel, out Window noteWindow))
            {
                noteWindow.Close();
            }
        }
    }
}
