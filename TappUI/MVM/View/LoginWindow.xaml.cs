using System.Windows;
using System.Windows.Input;
using TappUI.MVM.ViewModel;

namespace TappUI.MVM.View
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ButtonRequester_Click(object sender, RoutedEventArgs e)
        {
            LoginViewModel? vm = this.DataContext as LoginViewModel;

            if(vm != null)
            {
                vm.Login();
            }

            this.Close();
        }
    }
}
