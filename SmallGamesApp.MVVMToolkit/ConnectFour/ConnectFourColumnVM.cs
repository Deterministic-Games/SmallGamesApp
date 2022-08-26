using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace SmallGamesApp.MVVMToolkit.ConnectFour;

public partial class ConnectFourColumnVM : ObservableObject
{
    [ObservableProperty]
    private ConnectFourSquareVM? _first;

    [ObservableProperty]
    private ConnectFourSquareVM? _last;

    public ObservableCollection<ConnectFourSquareVM> Squares { get; set; } = new();

    public ConnectFourColumnVM() { }

    public void AddFirst()
    {
        var newFirst = new ConnectFourSquareVM()
        {
            Above = null,
            Below = _first
        };
        if (_first != null)
            _first.Above = newFirst;

        _first = newFirst;

        Squares.Insert(0, _first);
    }

    public void AddLast()
    {
        var newLast = new ConnectFourSquareVM()
        {
            Above = _last,
            Below = null
        };
        if (_last != null)
            _last.Below = newLast;

        _last = newLast;

        Squares.Add(newLast);
    }
}
