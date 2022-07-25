using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Models
{
    public struct Square
    {
        public bool IsWinningSquare { get; set; }
        public Player? Player { get; set; }

        public bool IsEmpty() => Player == null;
    }
}
