using System.Windows.Input;
using SmallGamesApp.Core.ViewModels;

namespace SmallGamesApp.Core.Minesweeper;

public class MinesweeperSquareViewModel : BaseViewModel
{
    #region Members

    public event EventHandler OpenEvent;
    public event EventHandler FlagEvent;

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
            //OnPropertyChanged("CanOpen");
            //OnPropertyChanged("CanFlag");
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
            //OnPropertyChanged("CanOpen");
            //OnPropertyChanged("CanFlag");
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

    private List<MinesweeperSquareViewModel> _neighbours = new();
    public List<MinesweeperSquareViewModel> Neighbours
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

    public ICommand OpenCommand { get; set; }
    public ICommand FlagCommand { get; set; }
    public ICommand ChordCommand { get; set; }

    #endregion

    #region Constructor

    public MinesweeperSquareViewModel(MinesweeperSquareModel square)
    {
        _square = square;
        Initialize();
    }

    public MinesweeperSquareViewModel()
    {
        _square = new MinesweeperSquareModel();
        Initialize();
    }

    private void Initialize()
    {
        OpenCommand = new RelayCommand(Open, () => CanOpen);
        FlagCommand = new RelayCommand(ToggleFlag, () => CanFlag);
        ChordCommand = new RelayCommand(Chord, () => CanChord);
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
