using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows;
using TappUI.MVM.View;

namespace TappUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
        }
    }
}
