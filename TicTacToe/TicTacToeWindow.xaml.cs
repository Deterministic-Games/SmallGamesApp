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

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for TicTacToeWindow.xaml
    /// </summary>
    public partial class TicTacToeWindow : Window
    {
        private TicTacToeModel _currentState = new();
        public TicTacToeModel CurrentState 
        { 
            get { return _currentState; }
            private set
            {
                _currentState = value;
                DataContext = value;
            }
        }

        public TicTacToeWindow(Window owner)
        {
            InitializeComponent();
            DataContext = _currentState;
            Owner = owner;
        }

        private void TicTacToeSquare_Click(object sender, RoutedEventArgs e)
        {
            CurrentState.CurrentTurn++;
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow = Owner;
            Application.Current.MainWindow.Show();
            Close();
        }

        private void RedoButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void UndoButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Application.Current.MainWindow != Owner) Owner.Close();
        }
    }
}
