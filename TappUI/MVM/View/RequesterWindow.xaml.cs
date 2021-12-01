using System.Windows;
using System.Windows.Input;
using TappUI.MVM.ViewModel;

namespace TappUI.MVM.View
{
    /// <summary>
    /// Interaction logic for RequesterWindow.xaml
    /// </summary>
    public partial class RequesterWindow : Window
    {
        public RequesterWindow()
        {
            InitializeComponent();
        }


        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void ButtonMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void ButtonWindowState_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState != WindowState.Maximized)
            {
                this.WindowState = WindowState.Maximized;
            }
            else
            {
                this.WindowState = WindowState.Normal;
            }
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ButtonFilter_Click(object sender, RoutedEventArgs e)
        {
            //ListProjects.Items.Refresh();
        }
    }
}
