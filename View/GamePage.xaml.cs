using SeaBattleWPF.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SeaBattleWPF.View
{
    /// <summary>
    /// Логика взаимодействия для GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        GameVM vm;
        public GamePage()
        {
            InitializeComponent();
            vm = (GameVM)DataContext;
            vm.RegisterField(fieldUser1, true);
            vm.RegisterField(fieldUser2, false);
            vm.RegisterDispatcher(Dispatcher);
        }

        private void MouseLeftClickFieldUser1(object sender, MouseButtonEventArgs e)
        {
            vm.ClickField(fieldUser1, e);
        }

        private void MouseLeftClickFieldUser2(object sender, MouseButtonEventArgs e)
        {
            vm.ClickField(fieldUser2, e);
        }
    }
}
