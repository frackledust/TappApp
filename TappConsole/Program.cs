using System;
using TappModels;
using TappService;

namespace TappConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Login login = new Login();
            
            bool isLogged = false;
            while(isLogged == false)
            {
                isLogged = login.Init();
            }


            while(true)
            {
                login.ShowProjects();

                Console.WriteLine("Choose: [f] for filter, [a] for show all or [q] for quit");
                ConsoleKeyInfo key = Console.ReadKey();

                if (key.KeyChar == 'f')
                {
                    Console.Clear();
                    Console.WriteLine("Choose commands: [translated]");
                    string commands = Console.ReadLine();


                    FilterService<Project> filterService = new FilterService<Project>();
                    login.Shown_projects = filterService.Filter(commands, login.User_projects);
                }
                else if (key.KeyChar == 'a')
                {
                    login.ShowAllProjects();
                }
                else if (key.KeyChar == 'q')
                {
                    Console.WriteLine("Thank you for using our app");
                    return;
                }
            }
        }
    }
}
