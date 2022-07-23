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
        private Window _parent;

        private TicTacToe _currentState = new();
        public TicTacToe Game 
        { 
            get { return _currentState; }
            private set
            {
                _currentState = value;
                DataContext = value;
            }
        }

        public TicTacToeWindow(Window parent)
        {
            InitializeComponent();
            DataContext = _currentState;
            _parent = parent;
        }

        private void TicTacToeSquare_Click(object sender, RoutedEventArgs e)
        {
            Game.CurrentTurn++;
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow = _parent;
            Application.Current.MainWindow.Show();
            Close();
        }
    }
}
