using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace SmallGamesApp.MVVMToolkit;

public partial class TicTacToeBoardVM : ObservableObject
{
    #region Properties

    public ObservableCollection<TicTacToeSquareVM> Squares { get; set; } = new();

    [ObservableProperty]
    private SquareState _currentPlayer;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsGameOver))]
    private SquareState _winner;

    public bool IsGameOver => _winner != SquareState.Empty;

    #endregion

    #region Constructor

    public TicTacToeBoardVM()
    {
        for (int i = 0; i < 9; i++)
        {
            Squares.Add(new TicTacToeSquareVM());
        }
        Initialize();
    }

    private void Initialize()
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

    [RelayCommand]
    private void Restart() => Initialize();

    public void CheckForWin()
    {
        var array = Squares.ToArray();

        var predicate = (TicTacToeSquareVM sqr) => sqr.State == _currentPlayer;

        // Check rows
        if (array[..3].All(predicate) || array[3..6].All(predicate) || array[6..9].All(predicate))
            Winner = _currentPlayer;

        // Check columns
        for (int col = 0; col < 3; col++)
        {
            if (array[col].State == _currentPlayer && array[col + 3].State == _currentPlayer && array[col + 6].State == _currentPlayer)
            {
                Debug.WriteLine("hheh");
                Winner = _currentPlayer;
            }
        }

        // Check diagonals
        if (array[4].State != _currentPlayer)
            return;

        if (array[0].State == _currentPlayer && array[8].State == _currentPlayer)
            Winner = _currentPlayer;

        if (array[2].State == _currentPlayer && array[6].State == _currentPlayer)
            Winner = _currentPlayer;
    }

    #endregion
}
