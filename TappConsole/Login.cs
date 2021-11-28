using System;
using System.Collections.ObjectModel;
using TappModels;
using TappService;

namespace TappConsole
{
    internal class Login
    {
        private int User_id;

        public string Username;
        public string User_role;

        public Project SelectedProject { get; set; }

        public Collection<Project> User_projects;
        public Collection<Project> Shown_projects;

        public void ShowProjects()
        {
            if (Shown_projects == null)
            {
                Console.WriteLine("No projects found");
            }

            Console.WriteLine();
            foreach (Project p in Shown_projects)
            {
                Console.WriteLine($"Project name: {p.Name}");
                Console.WriteLine($"Original language: {p.Original_language}");
                Console.WriteLine();
            }
        }

        public void ShowAllProjects()
        {
            Shown_projects = User_projects;
        }

        public bool Init()
        {
            Console.WriteLine("Welcome in the app");

            //Check role
            Console.WriteLine("Requester [r] or translator [t]?");
            ConsoleKeyInfo key = Console.ReadKey();

            if (key.KeyChar == 't')
            {
                User_role = "translator";
            }
            else if (key.KeyChar == 'r')
            {
                User_role = "requester";
            }
            else { return false; }

            //Get username
            Console.WriteLine("\nWrite your username:");
            Username = Console.ReadLine();

            if (Username == null) { return false; }

            User_projects = UserService.LoadProjects(Username, User_role);
            Shown_projects = User_projects;

            return true;
        }
    }
}
