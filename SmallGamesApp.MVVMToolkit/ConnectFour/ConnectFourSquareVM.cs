using CommunityToolkit.Mvvm.ComponentModel;

namespace SmallGamesApp.MVVMToolkit;

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
