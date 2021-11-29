﻿using System;
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

        public void RequesterLoop()
        {
            while (true)
            {
                ShowProjects();

                Console.WriteLine("Choose:");
                Console.WriteLine(">> [f] for filter");
                Console.WriteLine(">> [a] for show all");
                Console.WriteLine(">> [s] generate stats");
                Console.WriteLine(">> [q] for quit");

                ConsoleKeyInfo key = Console.ReadKey();

                Console.Clear();
                 
                if (key.KeyChar == 'f')
                {
                    Console.Clear();
                    Console.WriteLine("Choose commands:");
                    Console.WriteLine(">> [translated] to filter translated");
                    Console.WriteLine(">> [language-NAME-0/T], NAME = multiple language names, O - original language");
                    string commands = Console.ReadLine();


                    FilterService<Project> filterService = new FilterService<Project>();
                    Shown_projects = filterService.Filter(commands, User_projects);
                }
                else if (key.KeyChar == 'a')
                {
                    ShowAllProjects();
                }
                else if (key.KeyChar == 's')
                {
                    string file_path = StatsService.GenerateStatsToCSV(Shown_projects);
                    Console.WriteLine($"Find stats in file: {file_path}");
                }
                else if (key.KeyChar == 'q')
                {
                    Console.WriteLine("Thank you for using our app");
                    return;
                }
            }
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

            while(Username == null || Username.Length == 0)
            {
                Username = Console.ReadLine();
            }

            User_projects = UserService.LoadProjects(Username, User_role);
            Shown_projects = User_projects;

            return true;
        }
    }
}
