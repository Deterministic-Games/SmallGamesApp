using System.Windows.Input;
using Minesweeper.Models;
using Toolbox;

namespace Minesweeper.ViewModels;

public class SquareViewModel : BaseViewModel
{
    #region Wrapper
    private SquareModel _square;

    public bool HasMine
    {
        get { return _square.HasMine; }
        set
        {
            _square.HasMine = value;
            OnPropertyChanged();
        }
    }
    public bool IsFlagged
    {
        get { return _square.IsFlagged; }
        set
        {
            _square.IsFlagged = value;
            OnPropertyChanged();
            OnPropertyChanged("CanOpen");
            OnPropertyChanged("CanFlag");
        }
    }
    public bool IsOpened
    {
        get { return _square.IsOpened; }
        set
        {
            if (IsOpened) return;

            _square.IsOpened = value;
            OnPropertyChanged();
            OnPropertyChanged("CanOpen");
            OnPropertyChanged("CanFlag");
        }
    }
    public byte NeighbourMineCount
    {
        get { return _square.NeighbourMineCount; }
        set
        {
            _square.NeighbourMineCount = value;
            OnPropertyChanged();
        }
    }
    #endregion

    #region Commands
    public ICommand OpenCommand { get; init; }
    public ICommand FlagCommand { get; init; }
    #endregion

    public bool CanOpen => !(IsOpened || IsFlagged);
    public bool CanFlag => !IsOpened;

    #region Constructor
    public SquareViewModel(SquareModel square)
    {
        _square = square;
        OpenCommand = new RelayCommand(Open, () => CanOpen);
        FlagCommand = new RelayCommand(ToggleFlag, () => CanFlag);
    }

    public SquareViewModel()
    {
        _square = new SquareModel();
        OpenCommand = new RelayCommand(Open, () => CanOpen);
        FlagCommand = new RelayCommand(ToggleFlag, () => CanFlag);
    }
    #endregion

    public void Open()
    {
        IsOpened = true;
    }

    public void ToggleFlag()
    {
        IsFlagged = !IsFlagged;
    }
}
