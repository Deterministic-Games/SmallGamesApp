using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SmallGamesApp.MVVMToolkit.TicTacToe;

namespace SmallGamesApp.Core.TicTacToe;

public partial class TicTacToeSquareViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsOccupied))]
    [NotifyCanExecuteChangedFor(nameof(UpdateStateCommand))]
    private SquareState _state = SquareState.Empty;

    public bool IsOccupied => _state == SquareState.Empty;

    public TicTacToeSquareViewModel() { }

    [RelayCommand(CanExecute = nameof(IsOccupied))]
    private void UpdateState(TicTacToeBoardViewModel board)
    {
        var player = board.CurrentPlayer;
        State = player;
        board.CurrentPlayer = player == SquareState.Player1 ? SquareState.Player2 : SquareState.Player1;
    }
}
