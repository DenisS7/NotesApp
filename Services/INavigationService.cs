using NotesApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotesApp.Model;

namespace NotesApp.Services
{
    public interface INavigationService
    {
        void SaveNoteList();
        void CreateNote();
        void OpenNote(Note note);
        void CloseWindow(ViewModelBase viewModelBase);
    }
}
