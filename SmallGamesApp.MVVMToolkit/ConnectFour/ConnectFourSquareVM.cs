using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace SmallGamesApp.MVVMToolkit.ConnectFour;

public partial class ConnectFourSquareVM : ObservableObject
{
    #region Properties

    [ObservableProperty]
    private SquareState _state = SquareState.Empty;

    public bool IsAvailable => _state == SquareState.Empty;

    public int Column { get; init; }
    public int Row { get; init; }

    #endregion

    #region Constructor

    public ConnectFourSquareVM(int col, int row) 
    {
        Column = col;
        Row = row;
    }

    #endregion
}
