using System.Windows;
using Minesweeper.ViewModels;

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
            DataContext = new SquareViewModel();
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
