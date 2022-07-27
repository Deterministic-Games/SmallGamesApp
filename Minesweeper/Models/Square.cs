using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Models
{
    public struct Square
    {
        public bool HasMine { get; set; }
        public bool IsFlagged { get; set; }
        public bool IsOpened { get; set; }
        public byte NeighbourMineCount { get; set; }
    }
}
