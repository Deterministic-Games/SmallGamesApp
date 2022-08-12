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
            DataContext = new MinesweeperViewModel();
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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Application.Current.MainWindow != Owner) Owner.Close();
        }
    }
}
