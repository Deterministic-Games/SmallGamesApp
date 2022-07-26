﻿using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SmallGamesApp.Core;

public sealed class MinesweeperBoardVM : BaseViewModel
{
    #region Members

    private Timer? _timer;
    private TimerCallback _timerCallback;

    #endregion

    #region Wrapper

    private MinesweeperBoardModel _minesweeperModel;

    public MinesweeperBoardSize Size
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

    private ObservableCollection<MinesweeperSquareVM>? _squares;
    public ObservableCollection<MinesweeperSquareVM> Squares
    {
        get => _squares!;
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

    private ICommand? _openSeveralCommand;
    public ICommand OpenSeveralCommand => _openSeveralCommand ??= new ParameterizedRelayCommand(OpenSeveral);

    private ICommand? _restartCommand;
    public ICommand RestartCommand => _restartCommand ??= new RelayCommand(Restart);

    #endregion

    #region Constructor

    public MinesweeperBoardVM()
    {
        var size = MinesweeperBoardSize.Small;
        _minesweeperModel = new(size);
        Size = size;

        _timerCallback = new TimerCallback(Timer_Tick);

        Initialize();
    }

    public MinesweeperBoardVM(MinesweeperBoardSize size)
    {
        _minesweeperModel = new(size);
        Size = size;

        _timerCallback = new TimerCallback(Timer_Tick);

        Initialize();
    }

    public void Initialize()
    {
        Squares = new(_minesweeperModel.GetSquares().Select(sqr => new MinesweeperSquareVM(sqr)));

        foreach (var square in Squares)
        {
            square.Neighbours = GetNeighbours(square);
            square.OpenEvent += CheckWinOrLose;
            square.FlagEvent += UpdateMinesLeft;
        }
        _timer = new(_timerCallback, null, 1000, 1000);
        TimePlayed = 0;
    }

    #endregion

    #region Public methods

    public void OpenSeveral(object? obj)
    {
        if (obj == null) return;

        var sqr = (MinesweeperSquareVM)obj;

        if (!sqr.CanChord) return;

        HashSet<MinesweeperSquareVM> _visited = new();
        Crawl(sqr);

        void Crawl(MinesweeperSquareVM current)
        {
            _ = _visited.Add(current);
            foreach (var neighbour in current.Neighbours.Where(n => n.CanChord && !_visited.Contains(n)/* && n.NeighbourMineCount == 0*/ && !n.IsOpened))
            {
                Crawl(neighbour);
            }
            current.Chord();
        }
    }

    private void Restart()
    {
        _timer!.Dispose();
        _minesweeperModel.Restart();
        Initialize();
        OnPropertyChanged("MinesLeft");
        OnPropertyChanged("HasWon");
        OnPropertyChanged("HasLost");
    }

    #endregion

    #region Private methods

    private void UpdateMinesLeft(object? sender, EventArgs e) => OnPropertyChanged("MinesLeft");

    private void Timer_Tick(object? obj) => TimePlayed++;

    private void CheckWinOrLose(object? sender, EventArgs e)
    {
        if (HasLost)
        {
            _timer!.Dispose();
            foreach (var sqr in Squares.Where(s => s.HasMine))
            {
                sqr.IsOpened = true;
            }
            OnPropertyChanged("HasLost");
        }
        else if (HasWon)
        {
            _timer!.Dispose();
            foreach (var sqr in Squares.Where(s => !s.IsOpened))
                sqr.IsFlagged = true;
            OnPropertyChanged("MinesLeft");
            OnPropertyChanged("HasWon");
        }
    }

    private List<MinesweeperSquareVM> GetNeighbours(MinesweeperSquareVM square)
    {
        int index = Squares.IndexOf(square);
        (int row, int col) = Get2DIndex(index);

        var colMoreThanMin = col > 0;
        var colLessThanMax = col < Width - 1;
        var rowMoreThanMin = row > 0;
        var rowLessThanMax = row < Height - 1;

        var neighbours = new List<MinesweeperSquareVM>();

        if (colMoreThanMin) neighbours.Add(GetSquare(row, col - 1));

        if (colLessThanMax) neighbours.Add(GetSquare(row, col + 1));

        if (rowMoreThanMin) neighbours.Add(GetSquare(row - 1, col));

        if (rowLessThanMax) neighbours.Add(GetSquare(row + 1, col));

        if (colMoreThanMin && rowMoreThanMin) neighbours.Add(GetSquare(row - 1, col - 1));

        if (colMoreThanMin && rowLessThanMax) neighbours.Add(GetSquare(row + 1, col - 1));

        if (colLessThanMax && rowMoreThanMin) neighbours.Add(GetSquare(row - 1, col + 1));

        if (colLessThanMax && rowLessThanMax) neighbours.Add(GetSquare(row + 1, col + 1));

        return neighbours;

        MinesweeperSquareVM GetSquare(int row, int col) => Squares[(row * Width) + col];

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