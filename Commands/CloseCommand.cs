using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotesApp.Services;
using NotesApp.ViewModel;

namespace NotesApp.Commands
{
    public class CloseCommand : CommandBase
    {
        private NavigationService navigationService;

        public CloseCommand(NavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        public override void Execute(object? parameter)
        {
            if(parameter is ViewModelBase viewModelBase)
                navigationService.CloseWindow(viewModelBase);
        }
    }
}
