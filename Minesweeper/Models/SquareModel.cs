using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Minesweeper.Models;

public class SquareModel : INotifyPropertyChanged
{
    #region Member variables
    private bool _hasMine;
    public bool HasMine 
    { 
        get { return _hasMine; }
        set
        {
            _hasMine = value;
            OnPropertyChanged();
        }
    }
    private bool _isFlagged;
    public bool IsFlagged 
    {
        get { return _isFlagged; }
        set
        {
            _isFlagged = value;
            OnPropertyChanged();
        }
    }
    private bool _isOpened;
    public bool IsOpened 
    {
        get { return _isOpened; }
        set
        {
            _isOpened = value;
            OnPropertyChanged();
        }
    }
    private byte _neighbourMineCount;
    public byte NeighbourMineCount 
    {
        get { return _neighbourMineCount; }
        set
        {
            _neighbourMineCount = value;
            OnPropertyChanged();
        }
    }
    #endregion

    #region Propety Changed logic
    public event PropertyChangedEventHandler? PropertyChanged;
    internal void OnPropertyChanged([CallerMemberName] string? name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
    #endregion
}