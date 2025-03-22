using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotesApp.Model;
using NotesApp.Services;

namespace NotesApp.Commands
{
    public class CreateNoteCommand : CommandBase
    {
        private readonly NavigationService navigationService;
        public CreateNoteCommand(NavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        public override void Execute(object? parameter)
        {
            navigationService.CreateNote();
        }
    }
}
