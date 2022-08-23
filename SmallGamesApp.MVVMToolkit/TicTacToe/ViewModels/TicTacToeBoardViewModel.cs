﻿using CommunityToolkit.Mvvm.ComponentModel;
using SmallGamesApp.Core.TicTacToe;
using System.Collections.ObjectModel;

namespace SmallGamesApp.MVVMToolkit.TicTacToe;

public partial class TicTacToeBoardViewModel : ObservableObject
{
    #region Properties

    public ObservableCollection<TicTacToeSquareViewModel> Squares { get; set; } = new ObservableCollection<TicTacToeSquareViewModel>();

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsGameOver))]
    private SquareState _currentPlayer = SquareState.Player1;

    public bool IsGameOver => CheckForWin();

    #endregion

    #region Constructor

    public TicTacToeBoardViewModel()
    {
        for (int i = 0; i < 9; i++)
        {
            Squares.Add(new TicTacToeSquareViewModel());
        }
    }

    #endregion

    #region Private methods

    private bool CheckForWin()
    {
        var array = Squares.ToArray();

        var player = CurrentPlayer == SquareState.Player1 ? SquareState.Player2 : SquareState.Player1;

        var predicate = (TicTacToeSquareViewModel sqr) => sqr.State == player;

        // Check rows
        if (array[..3].All(predicate) || array[3..6].All(predicate) || array[6..9].All(predicate))
            return true;
        
        // Check columns
        for (int col = 0; col < 3; col++)
            if (array[col].State == player && array[col + 3].State == player && array[col + 6].State == player)
                return true;
        
        // Check diagonals
        if (array[4].State != player)
            return false;

        if (array[0].State == player && array[8].State == player)
            return true;

        return array[2].State == player && array[6].State == player;
    }

    #endregion
}