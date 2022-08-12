using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Models;

public class MinesweeperModel
{
    private List<SquareModel> _squares = new();

    public int Width { get; set; }
    public int Height { get; set; }
    public int Mines { get; set; }

    #region Constructor
    public MinesweeperModel(MinesweeperSize size)
    {
        Initialize(size);
    }

    public MinesweeperModel() 
    {
        Initialize(MinesweeperSize.Medium);
    }
    #endregion

    #region Initialization
    private void Initialize(MinesweeperSize size)
    {
        switch (size)
        {
            case MinesweeperSize.Small:
                Height = 9;
                Width = 9;
                Mines = 10;
                break;
            case MinesweeperSize.Medium:
                Height = 16;
                Width = 16;
                Mines = 40;
                break;
            case MinesweeperSize.Large:
                Height = 16;
                Width = 32;
                Mines = 99;
                break;
            default:
                break;
        }
        AddSquares();
        LayMines();
        GetNeighbouringMineCount();
    }

    private void AddSquares()
    {
        for (int i = 0; i < Width * Height; i++)
        {
            _squares.Add(SquareModel.CreateSquare());
        }
    }

    private void LayMines()
    {
        int bombs = Mines;
        while (bombs > 0)
        {
            var index = Random.Shared.Next(0, _squares.Count);

            if (!_squares[index].HasMine)
            {
                _squares[index].HasMine = true;
                bombs--;
            }
        }
    }
    #endregion

    public List<SquareModel> GetSquares() => _squares;

    public void GetNeighbouringMineCount()
    {
        for (int row = 0; row < Height; row++)
        {
            for (int col = 0; col < Width; col++)
            {
                var square = GetSquare(row, col);

                //if (square.HasMine) continue;

                /*  x x x
                 *  x s x
                 *  x x x
                 */
                bool colMoreThanMin = col > 1;
                bool colLessThanMax = col < Width - 1;
                bool rowMoreThanMin = row > 1;
                bool rowLessThanMax = row < Height - 1;

                if (colMoreThanMin && GetSquare(row, col - 1).HasMine) square.NeighbourMineCount++;

                if (colLessThanMax && GetSquare(row, col + 1).HasMine) square.NeighbourMineCount++;

                if (colMoreThanMin && rowMoreThanMin && GetSquare(row - 1, col - 1).HasMine) square.NeighbourMineCount++;

                if (colMoreThanMin && rowLessThanMax && GetSquare(row + 1, col - 1).HasMine) square.NeighbourMineCount++;


                if (rowMoreThanMin && GetSquare(row - 1, col).HasMine) square.NeighbourMineCount++;

                if (rowLessThanMax && GetSquare(row + 1, col).HasMine) square.NeighbourMineCount++;

                if (colLessThanMax && rowMoreThanMin && GetSquare(row - 1, col + 1).HasMine) square.NeighbourMineCount++;

                if (colLessThanMax && rowLessThanMax && GetSquare(row + 1, col + 1).HasMine) square.NeighbourMineCount++;
            }
        }
    }

    private SquareModel GetSquare(int row, int col) => _squares[FlattenIndex(row, col)];

    private int FlattenIndex(int row, int col) => (row * Width) + col;

    public override string ToString()
    {
        var sb = new StringBuilder();

        for (int row = 0; row < Height; row++)
        {
            for (int col = 0; col < Width; col++)
            {
                var square = GetSquare(row, col);

                var content = square.HasMine ? " " : square.NeighbourMineCount.ToString();
                sb.Append($" {content} ");
            }
            sb.Append('\n');
        }
        return sb.ToString();
    }
}
