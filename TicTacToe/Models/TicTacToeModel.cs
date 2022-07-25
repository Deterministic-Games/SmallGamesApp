using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace TicTacToe.Models
{
    public class TicTacToeModel : ICloneable, INotifyPropertyChanged
    {
        private uint _currentTurn;
        public uint CurrentTurn
        {
            get { return _currentTurn; }
            set
            {
                CurrentPlayer = value % 2 == 0 ? Player.X : Player.O;
                _currentTurn = value;
                OnPropertyChanged();
            }
        }
        private Player _currentPlayer;
        public Player CurrentPlayer
        {
            get { return _currentPlayer; }
            set
            {
                _currentPlayer = value;
                OnPropertyChanged();
            }
        }
        private bool _canUndo = false;
        public bool CanUndo
        {
            get { return _canUndo; }
            set
            {
                _canUndo = value;
                OnPropertyChanged();
            }
        }
        private bool _canRedo = false;
        public bool CanRedo
        {
            get { return _canRedo; }
            set
            {
                _canRedo = value;
                OnPropertyChanged();
            }
        }

        private Square[,] _squares = new Square[3, 3];
        public Square[,] Squares 
        { 
            get { return _squares; } 
            set
            {
                _squares = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public TicTacToeModel() { }

        public object Clone()
        {
            return new TicTacToeModel();
        }

        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
