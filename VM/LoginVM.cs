using SeaBattleWPF.API.Game;
using SeaBattleWPF.mvvm;
using SeaBattleWPF.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SeaBattleWPF.VM
{
    public class LoginVM : BaseVM
    {
        public string LoginText { get; set; }

        public CommandVM Login { get; set; }
        public CommandVM Registration { get; set; }

        public LoginVM()
        {
            Login = new CommandVM(() =>
              {
                  User user = User.TryLogin(LoginText, passwordBox.Password);
                  if (user != null)
                  {
                      PageControl.GetInstance().CurrentPage = new PageListGames();
                  }
              }
            );
            Registration = new CommandVM(() =>
               {
                   if (User.TryRegister(LoginText, passwordBox.Password))
                       Login.Execute(null);
               }
            );
        }
        PasswordBox passwordBox;
        internal void RegisterPassBox(PasswordBox passwordBox)
        {
            this.passwordBox = passwordBox;
        }


    }
}
