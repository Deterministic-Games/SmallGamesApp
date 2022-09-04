using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace SmallGamesApp.MVVMToolkit;

public partial class MemoryBoardVM : ObservableObject
{
    #region Properties

    public ObservableCollection<MemorySquareVM> Squares { get; } = new();

    private int _turn;
    public int Turn
    {
        get => _turn;
        set
        {
            _turn = value;
            OnPropertyChanged();

            if (value % 5 == 0)
                Squares.Add(new() { Position = Squares.Count });

            StartNextTurn();
        }
    }

    [ObservableProperty]
    private bool _isGameOver;

    #endregion

    #region Members

    private readonly List<int> _sequence = new();

    private int _sequenceNumber;

    #endregion

    public MemoryBoardVM()
    {
        for (int i = 0; i < 4; i++)
        {
            Squares.Add(new() { Position = i });
        }
        Turn = 1;
    }

    private async void StartNextTurn()
    {
        _sequenceNumber = 0;
        int i = Random.Shared.Next(Squares.Count);
        _sequence.Add(i);

        await Task.Delay(500);

        foreach (var num in _sequence)
        {
            await FlashSquare(Squares[num]);
        }
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
