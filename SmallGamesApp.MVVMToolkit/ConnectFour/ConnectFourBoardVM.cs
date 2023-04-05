using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace SmallGamesApp.MVVMToolkit;

public sealed partial class ConnectFourBoardVM : TwoPlayerBoardVM<ConnectFourSquareVM>
{
    #region Public properties

    public const int BoardRows = 6;
    public const int BoardColumns = 7;

    [ObservableProperty]
    private bool _isAITurn = false;

    #endregion

    #region Constructor

    public ConnectFourBoardVM()
	{
        for (int row = 0; row < BoardRows; row++)
        {
            for (int col = 0; col < BoardColumns; col++)
            {
                Squares.Add(new(col, row));
            }
        }
        Initialize();
	}

    protected override void Initialize()
    {
        foreach (var square in Squares)
        {
            square.State = SquareState.Empty;
        }
        CurrentPlayer = SquareState.Player1;
        Winner = SquareState.Empty;
    }

    #endregion

    #region Commands

    [RelayCommand]
	private async void PlaceToken(ConnectFourSquareVM sqr)
	{
		int col = sqr.Column;

        bool tokenPlaced = false;

		for (int r = BoardRows - 1; r >= 0; r--)
		{
			var square = Squares[FlattenIndex(r, col)];

            if (square.IsAvailable)
			{
				square.State = CurrentPlayer;
                CheckForWin(square);

                CurrentPlayer = PlayerSwitchMap[CurrentPlayer];
                tokenPlaced = true;
				break;
			}
		}

        if (tokenPlaced && Winner == SquareState.Empty)
        {
            IsAITurn = true;
            await MakeAIMove();
            IsAITurn = false;
        }
	}

    #endregion

    #region Private methods

    private async Task MakeAIMove()
    {
        int bestMove = await ConnectFourAI.GetBestMoveAsync(this, 4);
        TryPlaceTokenOnColumn(bestMove);
    }

    private void CheckForWin(ConnectFourSquareVM nSquare)
	{
		int row = nSquare.Row;
		int col = nSquare.Column;

		if (CheckRowForWin(row, col) || CheckDiagonals(row, col) || 
            (row <= 2 && CheckColumnForWin(row, col))
        )
        {
			Winner = CurrentPlayer;
		}
    }

	private bool CheckColumnForWin(int row, int col)
	{
		int StartIndex = FlattenIndex(row + 1, col);
		int maxIndex = FlattenIndex(row + 3, col);

		for (int i = StartIndex; i <= maxIndex; i += BoardColumns)
		{
			if (Squares[i].State != CurrentPlayer) return false;
		}
        return true;
	}

	private bool CheckRowForWin(int row, int col)
	{
		int connected = 0;

		int index = FlattenIndex(row, col);

        int minColumn = Math.Max(0, col - 3);
        int startIndex = FlattenIndex(row, minColumn);

        int maxColumn = Math.Min(6, col + 3);
        int endIndex = FlattenIndex(row, maxColumn);

        for (int i = startIndex; i <= endIndex; i++)
		{
            if (i == index)
                continue;

            connected = Squares[i].State == CurrentPlayer ? connected + 1 : 0;

            if (connected >= 3)
				return true;
        }
		return false;
    }

	private bool CheckDiagonals(int row, int col)
	{
		// Upper left <-> lower right
		int upperLeft = DiagonalConnected(row, col, -1, -1);
        int lowerRight = DiagonalConnected(row, col, 1, 1);

        if (upperLeft + lowerRight >= 3)
            return true;

		// Lower left <-> upper right
		int lowerLeft = DiagonalConnected(row, col, 1, -1);
        int upperRight = DiagonalConnected(row, col, -1, 1);

        if (lowerLeft + upperRight >= 3)
            return true;

        return false;
    }

    private int DiagonalConnected(int row, int col, int rowDirection, int colDirection)
    {
        int connected = 0;
        int currentRow = row + rowDirection;
        int currentCol = col + colDirection;

        while (currentRow >= 0 && currentRow <= BoardRows - 1 && currentCol >= 0 && currentCol <= BoardColumns - 1)
        {
            int index = FlattenIndex(currentRow, currentCol);

            if (Squares[index].State == CurrentPlayer)
            {
                connected++;
            }
            else
            {
                break;
            }

            currentRow += rowDirection;
            currentCol += colDirection;
        }

        return connected;
    }


    public int FlattenIndex(int row, int col) => (row * BoardColumns) + col;

    public ConnectFourBoardVM Copy()
    {
        return new ConnectFourBoardVM
        {
            CurrentPlayer = CurrentPlayer,
            Squares =
            new ObservableCollection<ConnectFourSquareVM>(
                Squares.Select(s => new ConnectFourSquareVM(s.Column, s.Row) { State = s.State })
            )
        };
    }

    #endregion

    #region Public methods

    public bool TryPlaceTokenOnColumn(int col)
    {
        for (int r = BoardRows - 1; r >= 0; r--)
        {
            var square = Squares[FlattenIndex(r, col)];

            if (square.IsAvailable)
            {
                square.State = CurrentPlayer;
                CheckForWin(square);

                CurrentPlayer = PlayerSwitchMap[CurrentPlayer];

                return true;
            }
        }
        return false;
    }

    #endregion
}
