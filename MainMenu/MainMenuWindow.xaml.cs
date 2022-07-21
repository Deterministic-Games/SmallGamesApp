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

using TicTacToe;

namespace MainMenu
{
    /// <summary>
    /// Interaction logic for MainMenuWindow.xaml
    /// </summary>
    public partial class MainMenuWindow : Window
    {
        public MainMenuWindow()
        {
            InitializeComponent();
        }

        private void TicTacToe_Click(object sender, RoutedEventArgs e)
        {
            SwitchWindow(new TicTacToeWindow());
        }

        private void SwitchWindow(Window window)
        {
            Application.Current.MainWindow = window;
            Application.Current.MainWindow.Show();
            Close();
        }

        private void GamesScrollViewer_MouseWheelOver(object sender, MouseWheelEventArgs e)
        {
            var scrollviewer = (ScrollViewer)sender;
            scrollviewer.ScrollToHorizontalOffset(scrollviewer.HorizontalOffset - e.Delta);
        }
    }
}
