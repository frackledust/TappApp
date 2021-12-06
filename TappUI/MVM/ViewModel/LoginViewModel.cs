using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TappUI.MVM.View;

namespace TappUI.MVM.ViewModel
{
    public class LoginViewModel
    {
        #region Fields
        public string Username { get; set; }

        public string Role {get; set; }

        #endregion

        #region Commands
        private ICommand _requesterLoginCommand;
        public ICommand RequesterLoginCommand
        {
            get { return _requesterLoginCommand ??= new RelayCommand(() => RequesterLogin(), true); }
        }

        private ICommand _translatorLoginCommand;
        public ICommand TranslatorLoginCommand
        {
            get { return _translatorLoginCommand ??= new RelayCommand(() => TranslatorLogin(), true); }
        }

        #endregion

        #region Methods
        private void Login()
        {
            if (Username != null && Username.Length > 0)
            {
                RequesterWindow r = new()
                {
                    DataContext = new UserViewModel(Username, Role),
                };
                r.Show();
            }
        }

        public void RequesterLogin()
        {
            Role = "requester";
            Login();
        }

        public void TranslatorLogin()
        {
            Role = "translator";
            Login();
        }

        #endregion
    }
}
