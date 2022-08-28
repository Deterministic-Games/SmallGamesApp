using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace SmallGamesApp.MVVMToolkit.ConnectFour;

public partial class ConnectFourSquareVM : ObservableRecipient
{
    #region Properties

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(UpdateStateCommand))]
    private SquareState _state = SquareState.Empty;

    public bool IsOccupied => _state != SquareState.Empty;

    public bool IsAvailable => Placement.row == 5 && !IsOccupied;

    public (int row, int col) Placement { get; init; }

    #endregion

    #region Constructor

    public ConnectFourSquareVM((int, int) placement) 
    {
        Placement = placement;
    }

    #endregion

    [RelayCommand(CanExecute = nameof(IsAvailable))]
    private void UpdateState() => State = SquareState.Player2;
}
