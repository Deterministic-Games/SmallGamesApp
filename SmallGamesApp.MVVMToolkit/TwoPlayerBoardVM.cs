using CommunityToolkit.Mvvm.ComponentModel;

namespace SmallGamesApp.MVVMToolkit;

public abstract partial class TwoPlayerBoardVM<T> : BaseBoardVM<T>
{
    #region Properies

    [ObservableProperty]
    private SquareState _currentPlayer = SquareState.Player1;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsGameOver))]
    private SquareState _winner = SquareState.Empty;

    public bool IsGameOver => _winner != SquareState.Empty;

    #endregion

    #region Members

    public static readonly IDictionary<SquareState, SquareState> PlayerSwitchMap = new Dictionary<SquareState, SquareState>
    {
        { SquareState.Player1, SquareState.Player2 },
        { SquareState.Player2, SquareState.Player1 }
    };

    #endregion
}
