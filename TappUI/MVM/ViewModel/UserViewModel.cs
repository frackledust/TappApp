using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using TappModels;
using TappService;

namespace TappUI.MVM.ViewModel
{
    class UserViewModel
    {
        #region Fields
        //Properties
        public int Id { get; set; }
        
        public string Username { get; set; }

        public ObservableCollection<Project> ShownProjects { get; set; }

        public Collection<Project> LoadedProjects { get; set; }

        public Project SelectedProject { get; set; }

        public string TextCommand { get; set; } // INotifyPropertyChanged

        public bool IsNotRequester { get;}

        #endregion

        #region Constructor
        public UserViewModel(string username, string role)
        {
            Username = username;

            IsNotRequester = (role != "requester");

            Id = LoginService.GetId(username);

            LoadedProjects = LoginService.LoadProjects(Id, role);

            ShownProjects = new ObservableCollection<Project>(LoadedProjects);
        }
        #endregion

        #region Commands
        private ICommand _filterCommand;
        public ICommand FilterCommand
        {
            get { return _filterCommand ?? (_filterCommand = new RelayCommand(() => Filter(), true)); }
        }

        private ICommand _statsCommand;
        public ICommand StatsCommand
        {
            get { return _statsCommand ?? (_statsCommand = new RelayCommand(() => Stats(), true)); }
        }

        private ICommand _createProjectCommand;
        public ICommand CreateProjectCommand
        {
            get { return _createProjectCommand ?? (_createProjectCommand = new RelayCommand(() => CreateProject(), true)); }
        }

        #endregion

        #region Methods
        public void Filter()
        {
            var projects = FilterService<Project>.Filter(TextCommand, LoadedProjects);
            ShownProjects.Clear();

            foreach (var project in projects)
            {
                ShownProjects.Add(project);
            }
        }

        public void Stats()
        {
            string file_path = StatsService.GenerateStatsToCSV(ShownProjects);
        }

        public void CreateProject()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                MessageBox.Show(openFileDialog.FileName);
                Project? added_project = ProjectService.CreateProject(openFileDialog.FileName, Id, false);

                if(added_project != null)
                {
                    ShownProjects.Add(added_project);
                    LoadedProjects.Add(added_project);
                }
                else
                {
                    MessageBox.Show("Project could not be created.\n Check if your file name is 'NAME_LANGUAGE_LANGUAGE.txt'");
                }
            }
        }
        #endregion
    }
}
