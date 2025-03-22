using NotesApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp.Services
{
    public interface INavigationService
    {
        void CreateNote();
        void OpenNote(NoteViewModel noteViewModel);
        void CloseNote(NoteViewModel noteViewModel);
    }
}
