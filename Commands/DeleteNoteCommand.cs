using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NotesApp.Model;
using NotesApp.Services;

namespace NotesApp.Commands
{
    class DeleteNoteCommand : CommandBase
    {
        private NavigationService navigationService;

        public DeleteNoteCommand(NavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        public override void Execute(object? parameter)
        {
            if (parameter is Note note)
            {
                navigationService.DeleteNote(note);
            }
        }
    }
}
