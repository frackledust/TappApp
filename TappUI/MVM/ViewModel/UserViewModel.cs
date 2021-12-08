using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using TappModels;
using TappService;

namespace TappUI.MVM.ViewModel
{
    /// <summary>
    /// Encapsulates data and controllers for UserView.xaml
    /// </summary>
    class UserViewModel
    {
        #region Fields
        public int Id { get; }

        public string Username { get; }

        public ObservableCollection<Project> ShownProjects { get; }

        public Collection<Project> LoadedProjects { get; }

        public Project SelectedProject { get; set; }

        public string TextCommand { get; set; }

        private string _helpText;
        public string HelpText
        {
            get { return _helpText ??= (_helpText = File.ReadAllText(@"assets\helptext.txt")); }
        }

        public bool IsNotRequester { get; }

        public bool IsTemporary { get; } = false;

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="UserViewModel"/> class.
        /// </summary>
        public UserViewModel(string username, string role)
        {
            Username = username;

            IsNotRequester = (role != "requester");

            Id = LoginService.GetId(username);

            if (Id > 0)
            {
                LoadedProjects = LoginService.LoadProjects(Id, role, true);
                ShownProjects = new ObservableCollection<Project>(LoadedProjects);
            }
            else
            {
                IsTemporary = true;
                LoadedProjects = new Collection<Project>();
                ShownProjects = new ObservableCollection<Project>();
            }
        }
        #endregion

        #region Commands
        private ICommand _createProjectCommand;
        public ICommand CreateProjectCommand
        {
            get { return _createProjectCommand ??= new RelayCommand(() => CreateProject(), true); }
        }

        private ICommand _filterCommand;
        public ICommand FilterCommand
        {
            get { return _filterCommand ??= new RelayCommand(() => Filter(), true); }
        }

        private ICommand _statsCommand;
        public ICommand StatsCommand
        {
            get { return _statsCommand ??= new RelayCommand(() => Stats(), true); }
        }

        private ICommand _reachTranslatorsCommand;
        public ICommand ReachTranslatorsCommand
        {
            get { return _reachTranslatorsCommand ??= new RelayCommand(() => ReachTranslators(), true); }
        }

        private ICommand _deactiveTranslatorCommand;
        public ICommand DeactiveTranslatorCommand
        {
            get { return _deactiveTranslatorCommand ??= new RelayCommand(() => DeactivateTranslator(), true); }
        }

        #endregion

        #region Methods
        public void CreateProject()
        {
            OpenFileDialog openFileDialog = new();

            if (openFileDialog.ShowDialog() == true)
            {
                Project added_project = ProjectService.CreateProject(openFileDialog.FileName, Id, IsTemporary);

                if (added_project != null)
                {
                    ShownProjects.Add(added_project);
                    LoadedProjects.Add(added_project);

                    if (IsTemporary)
                    {
                        MessageBox.Show($"Project added to temporary project list.");
                    }
                    else
                    {
                        MessageBox.Show($"Project added to the database with id {added_project.Id}'");
                    }
                }
                else
                {
                    MessageBox.Show("Project could not be created.\n Check if your file name is 'NAME_LANGUAGE_LANGUAGE.txt'");
                }
            }
        }

        public void Filter()
        {
            try
            {
                var projects = FilterService<Project>.Filter(TextCommand, LoadedProjects);
                ShownProjects.Clear();

                foreach (var project in projects)
                {
                    ShownProjects.Add(project);
                }
            }
            catch (Exception e) { MessageBox.Show(e.Message); return; }
        }

        public void Stats()
        {
            try
            {
                string file_path = StatsService.GenerateStatsToCSV(ShownProjects);
                MessageBox.Show($"Stats saved at {file_path}");
            }
            catch (Exception e) { MessageBox.Show(e.Message); return; }
        }

        public void ReachTranslators()
        {
            if (SelectedProject == null) { MessageBox.Show("Please select project first."); return; }

            if (SelectedProject.HasTranslation) { MessageBox.Show("Select project without translation."); return; }

            try
            {
                int translators_count = ContactService.ReachTranslators(Username, SelectedProject);
                MessageBox.Show($"Request sent to {translators_count} translators!");
            }
            catch (Exception e) { MessageBox.Show(e.Message); }
        }

        public void DeactivateTranslator()
        {
            try
            {
                UserService.DeactiveTranslator(Id);
                MessageBox.Show("Deactivation sucessful.");
                Application.Current.Shutdown();
            }
            catch (Exception e) { MessageBox.Show(e.Message); return; }
        }
        #endregion
    }
}
