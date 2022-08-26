using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace SmallGamesApp.MVVMToolkit.ConnectFour;

public partial class ConnectFourSquareVM : ObservableObject
{
    [ObservableProperty]
    private SquareState _state = SquareState.Empty;

    public bool IsOccupied => _state != SquareState.Empty;

    public bool IsAvailable => IsEmptyAbove();

    public ConnectFourSquareVM? Above { get; set; }

    public ConnectFourSquareVM? Below { get; set; }

    public ConnectFourSquareVM(ConnectFourSquareVM? above, ConnectFourSquareVM? below)
    {
        Above = above;
        Below = below;
    }

    public ConnectFourSquareVM() { }

    public bool IsEmptyAbove()
    {
        if (Above is null)
            return true;

        return Above.State is SquareState.Empty && Above.IsEmptyAbove();
    }

    [RelayCommand]
    private void UpdateState(SquareState state) => State = state;
}
