using System.Configuration;
using System.Data;
using System.Windows;
using NotesApp.Model;
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
            NoteList noteList = new NoteList();
            NavigationService navigationService = new NavigationService(noteList);
            navigationService.CreateNote();
            //MainWindow = new NoteView()
            //{
            //    DataContext = new NoteViewModel(navigationService, note)
            //};
            //MainWindow.Show();
            base.OnStartup(e);
        }
    }
}
