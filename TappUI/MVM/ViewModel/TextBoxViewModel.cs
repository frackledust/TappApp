using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TappUI.MVM.ViewModel
{
    internal class TextBoxViewModel
    {
        public string Prompt { get; set; }
        public string TextBox { get; set; }

        private ICommand _sendCommand;
        public ICommand SendCommand
        {
            get { return _sendCommand ??= new RelayCommand(() => Send(), true); }
        }

        public void Send()
        {

        }
    }
}
