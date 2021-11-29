using System;
using System.Collections.ObjectModel;
using TappModels;
using TappService;
using TappUI.MVM.Model;

namespace TappUI.MVM.ViewModel
{
    class RequesterViewModel
    {
        public ObservableCollection<ProjectModel> Projects { get; set; }

        public Project SelectedProject { get; set; }

        public RequesterViewModel(string username)
        {

            Projects = new ObservableCollection<ProjectModel>();

            Collection<Project> loaded_projects = UserService.LoadProjects(username, "requester");

            foreach (Project project in loaded_projects)
            {
                ProjectModel model = new ProjectModel();
                model.ProjectName = project.Name;
                model.RequesterName = username;

                Projects.Add(model);
            }

        }
    }
}
