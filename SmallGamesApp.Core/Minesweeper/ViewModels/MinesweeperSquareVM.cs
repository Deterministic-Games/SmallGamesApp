using System.Windows.Input;

namespace SmallGamesApp.Core;

public sealed class MinesweeperSquareVM : BaseViewModel
{
    #region Members

    public event EventHandler? OpenEvent;
    public event EventHandler? FlagEvent;

    #endregion

    #region Wrapper

    private readonly MinesweeperSquareModel _square;

    public bool HasMine
    {
        get => _square.HasMine;
        set
        {
            _square.HasMine = value;
            OnPropertyChanged();
        }
    }
    public bool IsFlagged
    {
        get => _square.IsFlagged;
        set
        {
            _square.IsFlagged = value;
            OnPropertyChanged();
            OnPropertyChanged("Image");
        }
    }
    public bool IsOpened
    {
        get => _square.IsOpened;
        set
        {
            if (IsOpened)
                return;

            _square.IsOpened = value;
            OnPropertyChanged();
            OnPropertyChanged("Image");
        }
    }
    public byte NeighbourMineCount
    {
        get => _square.NeighbourMineCount;
        set
        {
            _square.NeighbourMineCount = value;
            OnPropertyChanged();
        }
    }

    #endregion

    #region Properties

    public bool CanOpen => !(IsOpened || IsFlagged);
    public bool CanFlag => !IsOpened;
    public bool CanChord => _neighbours.FindAll(n => n.IsFlagged).Count == NeighbourMineCount;

    private List<MinesweeperSquareVM> _neighbours = new();
    public List<MinesweeperSquareVM> Neighbours
    {
        get => _neighbours;
        set
        {
            _neighbours = value;
            NeighbourMineCount = (byte)value.FindAll(sqr => sqr.HasMine).Count;
        }
    }

    public string Image
    {
        get
        {
            if (IsOpened)
                return HasMine ? "M" : ((NeighbourMineCount == 0) ? " " : NeighbourMineCount.ToString());
            
            return IsFlagged ? "F" : " ";
        }
    }

    #endregion

    #region Commands

    private ICommand? _openCommand;
    public ICommand OpenCommand => _openCommand ??= new RelayCommand(Open, () => CanOpen);

    private ICommand? _flagCommand;
    public ICommand FlagCommand => _flagCommand ??= new RelayCommand(ToggleFlag, () => CanFlag);

    private ICommand? _chordCommand;
    public ICommand ChordCommand => _chordCommand ??= new RelayCommand(Chord, () => CanChord);

    #endregion

    #region Constructor

    public MinesweeperSquareVM(MinesweeperSquareModel square)
    {
        _square = square;
    }

    #endregion

    #region Public methods

    public void Open()
    {
        IsOpened = true;
        OpenEvent?.Invoke(this, EventArgs.Empty);
    }

    public void ToggleFlag()
    {
        IsFlagged = !IsFlagged;
        FlagEvent?.Invoke(this, EventArgs.Empty);
    }

    public void Chord() => _neighbours.FindAll(n => !n.IsFlagged).ForEach(n => n.Open());

    #endregion
}
