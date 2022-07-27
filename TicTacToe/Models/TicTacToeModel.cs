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
    public class TicTacToeModel : INotifyPropertyChanged
    {
        private int _currentTurn = 1;
        public int CurrentTurn
        {
            get { return _currentTurn; }
            set
            {
                if (value < 1) return;

                _currentTurn = value;

                CurrentPlayer = value % 2 != 0 ? Player.X : Player.O;

                CanUndo = value > 1;
                CanRedo = value < States.Count;

                OnPropertyChanged();
            }
        }
        private Player _currentPlayer = Player.X;
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

        private Square[] _squares = new Square[9];
        public Square[] Squares 
        { 
            get { return _squares; } 
            set
            {
                _squares = value;
                OnPropertyChanged();
            }
        }

        public List<Square[]> States { get; set; } = new();

        public event PropertyChangedEventHandler? PropertyChanged;

        internal void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
