using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using System.Diagnostics;

using TicTacToe;
using Minesweeper;

namespace MainMenu
{
    /// <summary>
    /// Interaction logic for MainMenuWindow.xaml
    /// </summary>
    public partial class MainMenuWindow : Window
    {
        private double _currentOffsetAmount = 0.5;
        private double _lastOffsetAmount = 0.5;

        public MainMenuWindow()
        {
            InitializeComponent();
        }

        private void TicTacToe_Click(object sender, RoutedEventArgs e)
        {
            SwitchWindow(new TicTacToeWindow(this));
        }

        private void Minesweeper_Click(object sender, RoutedEventArgs e)
        {
            SwitchWindow(new MinesweeperWindow(this));
        }

        private void SwitchWindow(Window window)
        {
            Application.Current.MainWindow = window;
            Application.Current.MainWindow.Show();
            Hide();
        }

        #region GamesScrollViewer logic

        private void GamesScrollViewer_Loaded(object sender, RoutedEventArgs e)
        {
            Task scrollTask = PanScrollViewer();
        }

        private async Task PanScrollViewer()
        {
            while (true)
            {
                GamesScrollViewer.ScrollToHorizontalOffset(GamesScrollViewer.HorizontalOffset + _currentOffsetAmount);

                var cutoff = _currentOffsetAmount > 0 ? (GamesScrollViewer.ExtentWidth - GamesScrollViewer.ActualWidth) : 0;
                if (GamesScrollViewer.HorizontalOffset == cutoff)
                {
                    _currentOffsetAmount = -_currentOffsetAmount;
                }

                await Task.Delay(TimeSpan.FromMilliseconds(1));
            }
        }

        private void GamesScrollViewer_MouseLeave(object sender, MouseEventArgs e)
        {
            _currentOffsetAmount = _lastOffsetAmount;
        }

        private void GamesScrollViewer_MouseEnter(object sender, MouseEventArgs e)
        {
            _lastOffsetAmount = _currentOffsetAmount;
            _currentOffsetAmount = 0.0;
        }

        private void GamesScrollViewer_MouseWheelOver(object sender, MouseWheelEventArgs e)
        {
            var scrollviewer = (ScrollViewer)sender;
            scrollviewer.ScrollToHorizontalOffset(scrollviewer.HorizontalOffset - e.Delta);
        }

        #endregion
    }
}
