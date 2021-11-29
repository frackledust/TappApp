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

            if(login.User_role == "requester")
            {
                login.RequesterLoop();
            }
        }
    }
}
