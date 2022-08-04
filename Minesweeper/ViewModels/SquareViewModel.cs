using System.Windows.Input;
using Minesweeper.Commands;
using Minesweeper.Models;

namespace Minesweeper.ViewModels;

public class SquareViewModel
{
    public SquareModel Square { get; init; }

    public ICommand OpenCommand { get; init; }
    public ICommand FlagCommand { get; init; }

    public bool CanOpen => !(Square.IsOpened || Square.IsFlagged);
    public bool CanFlag => !Square.IsOpened;

    public SquareViewModel()
    {
        Square = new SquareModel();
        OpenCommand = new OpenSquareCommand(this);
        FlagCommand = new FlagSquareCommand(this);
    }

    public void Open()
    {
        Square.IsOpened = true;
        Square.NeighbourMineCount = 6;
    }

    public void ToggleFlag()
    {
        Square.IsFlagged = !Square.IsFlagged;
        Square.NeighbourMineCount = 1;
    }
}
