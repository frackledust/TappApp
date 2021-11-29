using System;
using System.Collections.ObjectModel;
using TappModels;
using TappService;
using TappUI.MVM.Model;

namespace TappUI.MVM.ViewModel
{
    class RequesterViewModel
    {
        public ObservableCollection<Project> ShownProjects { get; set; }

        public Project SelectedProject { get; set; }

        public RequesterViewModel(string username)
        {

            Collection<Project> loaded_projects = UserService.LoadProjects(username, "requester");
            
            ShownProjects = new ObservableCollection<Project>(loaded_projects);
        }
    }
}
