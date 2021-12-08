using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using TappModels;
using TappService;
using TappUI.MVM.View;

namespace TappUI.MVM.ViewModel
{
    /// <summary>
    /// Encapsulates data and controllers for UserView.xaml
    /// </summary>
    class UserViewModel
    {
        #region Fields
        /// <summary>
        /// Id of logged user
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Username of logged user
        /// </summary>
        public string Username { get; }

        /// <summary>
        /// Projects shown in the list on screen
        /// </summary>
        public ObservableCollection<Project> ShownProjects { get; }

        /// <summary>
        /// Projects of loaded user
        /// </summary>
        public Collection<Project> LoadedProjects { get; }

        /// <summary>
        /// Selected project from <see cref="ShownProjects"/>
        /// </summary>
        public Project SelectedProject { get; set; }

        /// <summary>
        /// String binded to textbox for addition commands
        /// </summary>
        public string TextCommand { get; set; }

        /// <summary>
        /// Text with information about app's functionality
        /// </summary>
        private string _helpText;
        public string HelpText
        {
            get { return _helpText ??= (_helpText = File.ReadAllText(@"assets\helptext.txt")); }
        }

        /// <summary>
        /// Shows if logged user is a requester
        /// </summary>
        public bool IsNotRequester { get; }

        /// <summary>
        /// Shows if logged user was found in database
        /// </summary>
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

        private ICommand _deleteProjectCommand;
        public ICommand DeleteProjectCommand
        {
            get { return _deleteProjectCommand ??= new RelayCommand(() => DeleteProject(), true); }
        }

        private ICommand _deactiveTranslatorCommand;
        public ICommand DeactiveTranslatorCommand
        {
            get { return _deactiveTranslatorCommand ??= new RelayCommand(() => DeactivateTranslator(), true); }
        }

        private ICommand _logoutCommand;
        public ICommand LogoutCommand
        {
            get { return _logoutCommand ??= new RelayCommand(() => Logout(), true); }
        }

        #endregion

        #region Methods
        /// <summary>
        /// Binded to Create project button
        /// </summary>
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
        /// <summary>
        /// Binded to Filter button of requester
        /// </summary>
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

        /// <summary>
        /// Binded to Stats button
        /// </summary>
        public void Stats()
        {
            try
            {
                string file_path = StatsService.GenerateStatsToCSV(ShownProjects);
                MessageBox.Show($"Stats saved at {file_path}");
            }
            catch (Exception e) { MessageBox.Show(e.Message); return; }
        }

        /// <summary>
        /// Binded to Reach Translators button of requester
        /// </summary>
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

        /// <summary>
        /// Binded to Delete Project button
        /// </summary>
        public void DeleteProject()
        {
            try
            {
                ProjectService.DeleteProject(SelectedProject);
                MessageBox.Show($"Project {SelectedProject.Name} sucessfully deleted.");
                LoadedProjects.Remove(SelectedProject);
                ShownProjects.Remove(SelectedProject);
            }
            catch (Exception e) { MessageBox.Show(e.Message); return; }
        }

        /// <summary>
        /// Binded to Give Up button of translator
        /// </summary>
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

        /// <summary>
        /// Binded to Logout button
        /// </summary>
        public void Logout()
        {
            try
            {
                if(IsTemporary == false)
                {
                    int count = ProjectService.SaveProjects(LoadedProjects);
                    MessageBox.Show($"{count} translation changes saved into database");
                }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message);}

            Window temp = Application.Current.MainWindow;
            Application.Current.MainWindow = new LoginWindow();
            Application.Current.MainWindow.Show();
            temp.Close();
        }
        #endregion
    }
}
