using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace SmallGamesApp.MVVMToolkit;

public sealed partial class MemoryBoardVM : BaseBoardVM<MemorySquareVM>
{
    #region Properties

    private int _turn;
    public int Turn
    {
        get => _turn;
        set
        {
            _turn = value;
            OnPropertyChanged();

            if (value % 5 == 0 && Squares.Count < 6)
                Squares.Add(new() { Position = Squares.Count });

            StartNextTurn();
        }
    }

    [ObservableProperty]
    private bool _isGameOver;

    /// <summary>
    /// A property to stop the player from pressing any buttons while the sequence is being shown
    /// </summary>
    [ObservableProperty]
    private bool _isNotShowingSequence;

    #endregion

    #region Members

    private readonly List<int> _sequence = new();

    private int _sequenceNumber;

    private readonly Random _rand = new();

    #endregion

    public MemoryBoardVM()
    {
        for (int i = 0; i < 4; i++)
            Squares.Add(new() { Position = i });
        
        Initialize();
    }

    protected override void Initialize()
    {
        if (_sequence.Count > 0)
        {
            _sequence.Clear();

            while (Squares.Count > 4)
                Squares.RemoveAt(4);
        }
        IsGameOver = false;
        Turn = 1;
    }

    private async void StartNextTurn()
    {
        _sequenceNumber = 0;
        int i = _rand.Next(Squares.Count);
        _sequence.Add(i);

        await Task.Delay(500);

        IsNotShowingSequence = false;

        foreach (var num in _sequence)
        {
            await FlashSquare(Squares[num]);
        }

        IsNotShowingSequence = true;
    }

    private async Task FlashSquare(MemorySquareVM square)
    {
        await Task.Delay(250);

        square.IsHighlighted = true;

        await Task.Delay(500);

        square.IsHighlighted = false;
    }

    [RelayCommand]
    private void PressSquare(MemorySquareVM square)
    {
        if (Squares.IndexOf(square) != _sequence[_sequenceNumber])
        {
            IsGameOver = true;
            return;
        }

        if (_sequenceNumber == Turn - 1)
            Turn++;
        else
            _sequenceNumber++;
    }
}
