using CommunityToolkit.Mvvm.ComponentModel;

namespace SmallGamesApp.MVVMToolkit;

public partial class MemorySquareVM : ObservableObject
{
    [ObservableProperty]
    private bool _isHighlighted;

    public int Position { get; init; }
}
