using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Models
{
    public class MinesweeperModel
    {
        // Bitmapped columns, 16 rows, 30 columns. Intermediate difficulty would be 16 columns.
        public Square[] Board { get; set; } = new Square[16 * 30];
        public int NumberOfMines { get; set; } = 99;

        public MinesweeperModel() { SpawnMines(); }
        public MinesweeperModel(int size)
        {
            Board = new Square[size];
            SpawnMines();
        }

        public void SpawnMines()
        {
            Board.Initialize();

            for (int i = 0; i < NumberOfMines; i++)
            {
                var index = Random.Shared.Next(Board.Length);
                while (Board[index].HasMine) index = Random.Shared.Next(Board.Length);
                Board[index] = true;
            }
        }
    }
}
