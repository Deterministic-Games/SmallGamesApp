using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace SmallGamesApp.MVVMToolkit;

public partial class TicTacToeBoardVM : TwoPlayerBoardVM<TicTacToeSquareVM>
{
    #region Constructor

    public TicTacToeBoardVM()
    {
        for (int i = 0; i < 9; i++)
        {
            Squares.Add(new TicTacToeSquareVM());
        }
        Initialize();
    }

    protected override void Initialize()
    {
        foreach (var square in Squares)
        {
            square.State = SquareState.Empty;
        }
        CurrentPlayer = SquareState.Player1;
        Winner = SquareState.Empty;
    }

    #endregion

    #region Private methods

    public void CheckForWin()
    {
        var array = Squares.ToArray();

        var player = CurrentPlayer;

        var predicate = (TicTacToeSquareVM sqr) => sqr.State == player;

        // Check rows
        if (array[..3].All(predicate) || array[3..6].All(predicate) || array[6..9].All(predicate))
            Winner = player;

        // Check columns
        for (int col = 0; col < 3; col++)
        {
            if (array[col].State == player && array[col + 3].State == player && array[col + 6].State == player)
                Winner = player;
        }

        // Check diagonals
        if (array[4].State != player)
            return;

        if (array[0].State == player && array[8].State == player)
            Winner = player;

        if (array[2].State == player && array[6].State == player)
            Winner = player;
    }

    #endregion
}
