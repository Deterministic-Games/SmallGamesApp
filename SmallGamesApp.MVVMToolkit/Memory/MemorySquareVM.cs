using CommunityToolkit.Mvvm.ComponentModel;

namespace SmallGamesApp.MVVMToolkit.Memory;

public partial class MemorySquareVM : ObservableObject
{
    [ObservableProperty]
    private bool _isHighlighted;

    public int Position { get; init; }
}
