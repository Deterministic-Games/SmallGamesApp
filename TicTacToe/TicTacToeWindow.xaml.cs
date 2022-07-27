using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
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
using TicTacToe.Models;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for TicTacToeWindow.xaml
    /// </summary>
    public partial class TicTacToeWindow : Window
    {
        private TicTacToeModel _gameModel = new();
        public TicTacToeModel GameModel 
        { 
            get { return _gameModel; }
            private set
            {
                _gameModel = value;
                DataContext = value;
            }
        }

        public TicTacToeWindow(Window owner)
        {
            InitializeComponent();
            DataContext = _gameModel;
            Owner = owner;
            GameModel.States.Add((Square[])GameModel.Squares.Clone());
        }

        #region Game button control / logic
        private void TicTacToeSquare_Click(object sender, RoutedEventArgs e)
        {
            // Do nothing if a player has already occupied the square
            var button = (Button)sender;
            if (button.Content is Player) return;

            // Set clicked square's player to the current player
            var tag = (string)button.Tag;
            var index = int.Parse(tag);
            GameModel.Squares[index].Player = GameModel.CurrentPlayer;

            // Manage states
            if (GameModel.CanRedo)
            {
                GameModel.States = GameModel.States.GetRange(0, GameModel.CurrentTurn);
            }
            GameModel.States.Add((Square[])GameModel.Squares.Clone());

            // Handle win
            CheckWin();

            GameModel.CurrentTurn++;

            // Notify change to update UI
            GameModel.OnPropertyChanged("Squares");
        }

        private void CheckWin()
        {
            if (HasWon(Player.X))
            {
                GameGrid.IsHitTestVisible = false;
                WinLabel.Content = "Player X won!";
            }
            else if (HasWon(Player.O))
            {
                GameGrid.IsHitTestVisible = false;
                WinLabel.Content = "Player O won!";
            }
            else
            {
                GameGrid.IsHitTestVisible = true;
                WinLabel.Content = null;
            }
        }

        public bool HasWon(Player player)
        {
            var squares = GameModel.Squares;
            for (int row = 0; row < 3; row++)
            {
                // Check row
                var start = row * 3;
                if (squares[start].Player == player && squares[start + 1].Player == player && squares[start + 2].Player == player)
                {
                    SetWinningSquares(start, start + 1, start + 2);
                    return true;
                }

                // Check column
                if (squares[row].Player == player && squares[row + 3].Player == player && squares[row + 6].Player == player)
                {
                    SetWinningSquares(row, row + 3, row + 6);
                    return true;
                }
            }
            // Check diagonals
            if (squares[4].Player != player) return false;

            if (squares[0].Player == player && squares[8].Player == player)
            {
                SetWinningSquares(0, 4, 8);
                return true;
            }

            if (squares[2].Player == player && squares[6].Player == player)
            {
                SetWinningSquares(2, 4, 6);
                return true;
            }
            return false;
        }

        private void SetWinningSquares(int index1, int index2, int index3)
        {
            var squares = GameModel.Squares;
            squares[index1].IsWinningSquare = true;
            squares[index2].IsWinningSquare = true;
            squares[index3].IsWinningSquare = true;
        }
        #endregion

        #region Menu button controls
        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow = Owner;
            Application.Current.MainWindow.Show();
            Close();
        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            GameModel = new();
            GameGrid.IsHitTestVisible = true;
            GameModel.States.Add((Square[])GameModel.Squares.Clone());
        }

        private void RedoButton_Click(object sender, RoutedEventArgs e)
        {
            GameModel.CurrentTurn++;
            ChangeState();
        }

        private void UndoButton_Click(object sender, RoutedEventArgs e)
        {
            GameModel.CurrentTurn--;;
            ChangeState();
        }

        private void ChangeState()
        {
            GameModel.States[GameModel.CurrentTurn - 1].CopyTo(GameModel.Squares, 0);
            CheckWin();
            GameModel.OnPropertyChanged("Squares");
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Application.Current.MainWindow != Owner) Owner.Close();
        }
        #endregion
    }
}
