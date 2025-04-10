using System.Configuration;
using System.Data;
using System.Windows;
using NotesApp.Model;
using NotesApp.Saving;
using NotesApp.Services;
using NotesApp.View;
using NotesApp.ViewModel;

namespace NotesApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            NoteList noteList = NoteListSavingUtility.LoadNoteList();
            NavigationService navigationService = new NavigationService(noteList);
            navigationService.OpenNoteList();
            base.OnStartup(e);
        }
    }
}
