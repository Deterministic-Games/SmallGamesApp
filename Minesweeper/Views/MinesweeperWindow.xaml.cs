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
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Application.Current.MainWindow != Owner) Owner.Close();
        }
    }
}
