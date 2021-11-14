using System;
using System.Collections.ObjectModel;
using TappUI.MVM.Model;

namespace TappUI.MVM.ViewModel
{
    class RequesterViewModel
    {
        public ObservableCollection<ProjectModel> Projects { get; set; }

        public RequesterViewModel()
        {
            Projects = new ObservableCollection<ProjectModel>();


            //Data for visibility sake
            for (int i = 0; i < 4; i++)
            {
                Projects.Add(new ProjectModel
                {
                    ProjectName = $"Project {i}",
                    RequesterName = "Unknown",
                });

                i++;

                Projects.Add(new ProjectModel
                {
                    ProjectName = $"Project {i}",
                    RequesterName = "Zach",
                });
            }

        }
    }
}
