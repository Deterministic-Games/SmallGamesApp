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

    public TicTacToeSquareVM() { }

    [RelayCommand(CanExecute = nameof(IsOccupied))]
    private void UpdateState(TicTacToeBoardVM board)
    {
        var player = board.CurrentPlayer;
        State = player;
        board.CheckForWin();
        board.CurrentPlayer = TwoPlayerBoardVM<object>.PlayerSwitchMap[board.CurrentPlayer];

    }
}
