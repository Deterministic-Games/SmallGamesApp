using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TicTacToe.Models
{
    public struct Square
    {
        public bool IsWinningSquare { get; set; }
        public Player? Player { get; set; }
        public Brush Color { get => IsWinningSquare ? Brushes.Red : Brushes.Black; }
        public bool IsEmpty { get => Player == null; }
    }
}
