using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Threading;
using Minesweeper.Models;
using Toolbox;

namespace Minesweeper.ViewModels;

public class MinesweeperViewModel : BaseViewModel
{
    #region Members

    private readonly DispatcherTimer _timer = new() { Interval = new TimeSpan(0, 0, 1) };

    #endregion

    #region Wrapper

    private MinesweeperModel _minesweeperModel;

    public MinesweeperSize Size
    {
        get => _minesweeperModel.Size;
        set
        {
            _minesweeperModel.Size = value;
            OnPropertyChanged();
        }
    }

    public int Width
    {
        get => _minesweeperModel.Width;
        set
        {
            _minesweeperModel.Width = value;
            OnPropertyChanged();
        }
    }
    public int Height
    {
        get => _minesweeperModel.Height;
        set
        {
            _minesweeperModel.Height = value;
            OnPropertyChanged();
        }
    }
    public int Mines
    {
        get => _minesweeperModel.Mines;
        set
        {
            _minesweeperModel.Mines = value;
            OnPropertyChanged();
        }
    }

    public bool HasWon => _minesweeperModel.HasWon;
    public bool HasLost => _minesweeperModel.HasLost;

    #endregion

    #region Properties

    private ObservableCollection<SquareViewModel> _squares;
    public ObservableCollection<SquareViewModel> Squares
    {
        get => _squares;
        set
        {
            _squares = value;
            OnPropertyChanged();
        }
    }

    public int MinesLeft => Math.Max(Mines - Squares.Where(sqr => sqr.IsFlagged).Count(), 0);

    private int _timePlayed;
    public int TimePlayed
    {
        get => _timePlayed;
        set
        {
            _timePlayed = value;
            OnPropertyChanged();
        }
    }

    #endregion

    #region Commands

    public ICommand OpenSeveralCommand { get; set; }
    public ICommand RestartCommand { get; set; }

    #endregion

    #region Constructor

    public MinesweeperViewModel()
    {
        var size = MinesweeperSize.Small;
        _minesweeperModel = new(size);
        Size = size;

        _timer.Tick += new EventHandler(Timer_Tick);
        Initialize();
    }

    public MinesweeperViewModel(MinesweeperSize size)
    {
        _minesweeperModel = new(size);
        Size = size;

        _timer.Tick += new EventHandler(Timer_Tick);
        Initialize();
    }

    public void Initialize()
    {
        Squares = new(_minesweeperModel.GetSquares().Select(sqr => new SquareViewModel(sqr)));

        OpenSeveralCommand = new ParameterizedRelayCommand(OpenSeveral);
        RestartCommand = new RelayCommand(Restart);

        foreach (var square in Squares)
        {
            square.Neighbours = GetNeighbours(square);
            square.OpenEvent += CheckWinOrLose;
            square.FlagEvent += UpdateMinesLeft;
        }
        TimePlayed = 0;
        _timer.Start();
    }

    #endregion

    #region Public methods

    public void OpenSeveral(object? obj)
    {
        if (obj == null) return;

        var sqr = (SquareViewModel)obj;

        if (!sqr.CanChord) return;

        HashSet<SquareViewModel> _visited = new();
        Crawl(sqr);

        void Crawl(SquareViewModel current)
        {
            current.Chord();
            _ = _visited.Add(current);

            foreach (var neighbour in current.Neighbours.Where(n => n.CanChord && !_visited.Contains(n)/* && n.NeighbourMineCount == 0*/))
            {
                Crawl(neighbour);
            }
        }
    }

    public void Restart()
    {
        _timer.Stop();

        _minesweeperModel.Restart();
        Initialize();
        OnPropertyChanged("MinesLeft");
        OnPropertyChanged("HasWon");
        OnPropertyChanged("HasLost");
    }

    #endregion

    #region Private methods

    private void UpdateMinesLeft(object? sender, EventArgs e) => OnPropertyChanged("MinesLeft");

    private void Timer_Tick(object? sender, EventArgs e) => TimePlayed++;

    private void CheckWinOrLose(object? sender, EventArgs e)
    {
        if (HasLost)
        {
            foreach (var sqr in Squares.Where(s => s.HasMine))
            {
                sqr.IsOpened = true;
            }
            OnPropertyChanged("HasLost");
        }
        else if (HasWon)
        {
            _timer.Stop();
            foreach (var sqr in Squares.Where(s => !s.IsOpened))
                sqr.IsFlagged = true;
            OnPropertyChanged("MinesLeft");
            OnPropertyChanged("HasWon");
        }
    }

    private List<SquareViewModel> GetNeighbours(SquareViewModel square)
    {
        int index = Squares.IndexOf(square);
        (int row, int col) = Get2DIndex(index);

        var colMoreThanMin = col > 0;
        var colLessThanMax = col < Width - 1;
        var rowMoreThanMin = row > 0;
        var rowLessThanMax = row < Height - 1;

        var neighbours = new List<SquareViewModel>();

        if (colMoreThanMin) neighbours.Add(GetSquare(row, col - 1));

        if (colLessThanMax) neighbours.Add(GetSquare(row, col + 1));

        if (rowMoreThanMin) neighbours.Add(GetSquare(row - 1, col));

        if (rowLessThanMax) neighbours.Add(GetSquare(row + 1, col));

        if (colMoreThanMin && rowMoreThanMin) neighbours.Add(GetSquare(row - 1, col - 1));

        if (colMoreThanMin && rowLessThanMax) neighbours.Add(GetSquare(row + 1, col - 1));

        if (colLessThanMax && rowMoreThanMin) neighbours.Add(GetSquare(row - 1, col + 1));

        if (colLessThanMax && rowLessThanMax) neighbours.Add(GetSquare(row + 1, col + 1));

        return neighbours;

        SquareViewModel GetSquare(int row, int col) => Squares[(row * Width) + col];

        (int, int) Get2DIndex(int index)
        {
            int row = 0;
            while (index > Width - 1)
            {
                row++;
                index -= Width;
            }
            return (row, index);
        }
    }

    #endregion
}