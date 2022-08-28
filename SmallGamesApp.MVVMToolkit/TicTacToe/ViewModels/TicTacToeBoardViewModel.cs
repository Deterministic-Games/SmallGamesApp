using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SmallGamesApp.Core.TicTacToe;
using System.Collections.ObjectModel;

namespace SmallGamesApp.MVVMToolkit.TicTacToe;

public partial class TicTacToeBoardViewModel : ObservableObject
{
    #region Properties

    public ObservableCollection<TicTacToeSquareViewModel> Squares { get; set; } = new ObservableCollection<TicTacToeSquareViewModel>();

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsGameOver))]
    private SquareState _currentPlayer;

    public bool IsGameOver => CheckForWin();

    [ObservableProperty]
    private SquareState _winner;

    private static readonly IDictionary<SquareState, SquareState> s_playerMap = new Dictionary<SquareState, SquareState>
    {
        { SquareState.Player1, SquareState.Player2 },
        { SquareState.Player2, SquareState.Player1 }
    };

    #endregion

    #region Constructor

    public TicTacToeBoardViewModel()
    {
        for (int i = 0; i < 9; i++)
        {
            Squares.Add(new TicTacToeSquareViewModel());
        }
        Initialize();
    }

    private void Initialize()
    {
        foreach (var square in Squares)
        {
            square.State = SquareState.Empty;
        }
        CurrentPlayer = SquareState.Empty;
        CurrentPlayer = SquareState.Player1;
        Winner = SquareState.Empty;
    }

    #endregion

    #region Private methods

    [RelayCommand]
    private void Restart() => Initialize();

    private bool CheckForWin()
    {
        var array = Squares.ToArray();

        //var player = CurrentPlayer == SquareState.Player1 ? SquareState.Player2 : SquareState.Player1;

        var player = s_playerMap[CurrentPlayer];

        var predicate = (TicTacToeSquareViewModel sqr) => sqr.State == player;

        // Check rows
        if (array[..3].All(predicate) || array[3..6].All(predicate) || array[6..9].All(predicate))
        {
            Winner = player;
            return true;
        }

        // Check columns
        for (int col = 0; col < 3; col++)
        {
            if (array[col].State == player && array[col + 3].State == player && array[col + 6].State == player)
            {
                Winner = player;
                return true;
            }
        }

        // Check diagonals
        if (array[4].State != player)
            return false;

        if (array[0].State == player && array[8].State == player)
        {
            Winner = player;
            return true;
        }

        if (array[2].State == player && array[6].State == player)
        {
            Winner = player;
            return true;
        }

        return false;
    }

    #endregion
}
