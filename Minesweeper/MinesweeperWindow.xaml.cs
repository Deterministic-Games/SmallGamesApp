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
using System.Windows.Shapes;

namespace Minesweeper
{
    /// <summary>
    /// Interaction logic for MinesweeperWindow.xaml
    /// </summary>
    public partial class MinesweeperWindow : Window
    {
        public MinesweeperWindow(Window owner)
        {
            InitializeComponent();
            Owner = owner;
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow = Owner;
            Application.Current.MainWindow.Show();
            Close();
        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
