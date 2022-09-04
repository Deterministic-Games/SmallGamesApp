using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace SmallGamesApp.MVVMToolkit;

public partial class TicTacToeSquareVM : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsOccupied))]
    [NotifyCanExecuteChangedFor(nameof(UpdateStateCommand))]
    private SquareState _state = SquareState.Empty;

    public bool IsOccupied => _state == SquareState.Empty;

    private static readonly IDictionary<SquareState, SquareState> s_playerMap = new Dictionary<SquareState, SquareState>
    {
        { SquareState.Player1, SquareState.Player2 },
        { SquareState.Player2, SquareState.Player1 }
    };

    public TicTacToeSquareVM() { }

    [RelayCommand(CanExecute = nameof(IsOccupied))]
    private void UpdateState(TicTacToeBoardVM board)
    {
        var player = board.CurrentPlayer;
        State = player;
        board.CheckForWin();
        board.CurrentPlayer = s_playerMap[board.CurrentPlayer];
    }
}
