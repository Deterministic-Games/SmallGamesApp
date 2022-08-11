using System;
using System.Collections.Generic;
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
}
