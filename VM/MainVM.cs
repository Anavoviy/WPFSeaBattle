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
    public class MainVM : BaseVM
    {
        public PageControl PageControl { get; set; }
        
        public MainVM()
        {
            PageControl = PageControl.GetInstance();
            PageControl.CurrentPage = new LoginPage();
            Application.Current.Exit += PageControl.OnAppClose;
        }
    }
}
