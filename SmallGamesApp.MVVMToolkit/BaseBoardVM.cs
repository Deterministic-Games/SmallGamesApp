using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace SmallGamesApp.MVVMToolkit;

public abstract partial class BaseBoardVM<T> : ObservableObject
{
    public ObservableCollection<T> Squares { get; set; } = new();

    protected abstract void Initialize();

    [RelayCommand]
    private void Restart() => Initialize();
}
