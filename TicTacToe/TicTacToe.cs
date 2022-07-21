using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace TicTacToe
{
    public class TicTacToe : ICloneable, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private uint _currentTurn;
        public uint CurrentTurn
        {
            get { return _currentTurn; }
            set
            {
                CurrentPlayer = value % 2 == 0 ? Player.X : Player.O;
                _currentTurn = value;
            }
        }
        public Player CurrentPlayer { get; set; }

        public bool[,] HasX { get; set; } = new bool[3, 3];
        public bool[,] HasO { get; set; } = new bool[3, 3];

        public bool CanUndo { get; set; } = false;
        public bool CanRedo { get; set; } = false;

        public TicTacToe() { }

        public bool HasWon(bool playerX)
        {
            var markedSquares = playerX ? HasX : HasO;

            for (int i = 0; i < 3; i++)
            {
                // Vertical
                if (markedSquares[0, i] && markedSquares[1, i] && markedSquares[2, i]) return true;

                // Horizontal
                if (markedSquares[i, 0] && markedSquares[i, 1] && markedSquares[i, 2]) return true;
            }
            // Diagonal
            if (markedSquares[1, 1]) return false;

            if (markedSquares[0, 0] && markedSquares[2, 2]) return true;

            if (markedSquares[2, 0] && markedSquares[0, 2]) return true;

            return false;
        }

        public object Clone()
        {
            var clone = new TicTacToe
            {
                HasX = (bool[,])this.HasX.Clone(),
                HasO = (bool[,])this.HasO.Clone(),
                CurrentTurn = this.CurrentTurn
            };
            return clone;
        }

        public struct Square
        {
            public bool IsEmpty { get; set; }
            public bool IsWinningSquare { get; set; }
            public Player Player { get; set; }

        }

        public enum Player
        {
            X,
            O
        }
    }
}
