using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TappModels;
using TappService;

namespace TappUI.MVM.ViewModel
{
    class RequesterViewModel
    {
        //Properties
        public ObservableCollection<Project> ShownProjects { get; set; }

        public Collection<Project> LoadedProjects { get; set; }

        public Project SelectedProject { get; set; }

        public string TextCommand { get; set; } // INotifyPropertyChanged

        public RequesterViewModel(string username)
        {
            LoadedProjects = UserService.LoadProjects(username, "requester");
            ShownProjects = new ObservableCollection<Project>(LoadedProjects);
        }

        //Comands

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

        //Methods

        public void Filter()
        {
            FilterService<Project> filterService = new FilterService<Project>();
            var projects = filterService.Filter(TextCommand, LoadedProjects);
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
    }
}
