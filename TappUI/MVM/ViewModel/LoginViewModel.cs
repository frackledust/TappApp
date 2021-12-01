using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TappUI.MVM.View;

namespace TappUI.MVM.ViewModel
{
    public class LoginViewModel
    {
        public string Username { get; set; }

        public void Login()
        {
            if (Username != null && Username.Length > 0)
            {
                RequesterWindow r = new RequesterWindow()
                {
                    DataContext = new RequesterViewModel(Username),
                };
                r.Show();
            }
        }
    }
}
