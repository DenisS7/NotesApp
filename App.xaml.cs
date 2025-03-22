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
            noteList.CreateNote();
            noteList.CreateNote();
            noteList.CreateNote();
            NavigationService navigationService = new NavigationService(noteList);
            navigationService.OpenMenu();
            //MainWindow = new NoteListView()
            //{
            //    DataContext = new NoteListViewModel(navigationService, noteList)
            //};
            //MainWindow.Show();
            base.OnStartup(e);
        }
    }
}
