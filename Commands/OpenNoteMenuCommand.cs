﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotesApp.Services;

namespace NotesApp.Commands
{
    class OpenNoteMenuCommand : CommandBase
    {
        private NavigationService navigationService;

        public OpenNoteMenuCommand(NavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        public override void Execute(object? parameter)
        {
            navigationService.OpenMenu();
        }
    }
}
