using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace SmallGamesApp.MVVMToolkit;

public partial class ConnectFourBoardVM : TwoPlayerBoardVM<ConnectFourSquareVM>
{
    #region Constructor

    public ConnectFourBoardVM()
	{
        for (int row = 0; row < 6; row++)
        {
            for (int col = 0; col < 7; col++)
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
	private void PlaceToken(ConnectFourSquareVM sqr)
	{
		int col = sqr.Column;

		for (int r = 5; r >= 0; r--)
		{
			var square = Squares[FlattenIndex(r, col)];

            if (square.IsAvailable)
			{
				square.State = CurrentPlayer;
                CheckForWin(square);

                CurrentPlayer = PlayerSwitchMap[CurrentPlayer];
				break;
			}
		}
	}

    #endregion

    #region Private methods

    private void CheckForWin(ConnectFourSquareVM nSquare)
	{
		int row = nSquare.Row;
		int col = nSquare.Column;

		if (CheckRowForWin(row, col)) // Check row
        {
			Winner = CurrentPlayer;
		}
        else if (CheckDiagonals(row, col)) // Check diagonals
        {
            Winner = CurrentPlayer;
        }
        else if (row <= 2) // Check column
		{
            if (CheckColumnForWin(row, col))
                Winner = CurrentPlayer;
        }
    }

	private bool CheckColumnForWin(int row, int col)
	{
		int StartIndex = FlattenIndex(row + 1, col);
		int maxIndex = FlattenIndex(row + 3, col);

		for (int i = StartIndex; i <= maxIndex; i += 7)
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
        int index = FlattenIndex(row, col);

        int rowsDown = Math.Min(3, 5 - row);
        int rowsUp = Math.Min(3, row);
        int rowsLeft = Math.Min(3, col);
        int rowsRight = Math.Min(3, 6 - col);

		// Upper left <-> lower right
		int upperLeft = UpwardDiagonalConnected(rowsUp, rowsLeft, 8, index);
		int lowerRight = DownwardDiagonalConnected(rowsDown, rowsRight, 8, index);

        if (upperLeft + lowerRight >= 3)
            return true;

		// Lower left <-> upper right
		int lowerLeft = DownwardDiagonalConnected(rowsDown, rowsLeft, 6, index);
		int upperRight = UpwardDiagonalConnected(rowsUp, rowsRight, 6, index);

        if (lowerLeft + upperRight >= 3)
            return true;

        return false;
    }

	public int UpwardDiagonalConnected(int rows, int cols, int offset, int index)
	{
        int connected = 0;
        int maxOffset = Math.Min(rows, cols);

        if (maxOffset == 0)
            return 0;

        int minIndex = index - (maxOffset * offset);

        for (int i = index - offset; i >= minIndex; i -= offset)
        {
            if (Squares[i].State != CurrentPlayer)
                break;
            connected++;
        }
        return connected;
    }

    public int DownwardDiagonalConnected(int rows, int cols, int offset, int index)
    {
        int connected = 0;
        int maxOffset = Math.Min(rows, cols);

        if (maxOffset == 0)
            return 0;

        int maxIndex = index + (maxOffset * offset);

        for (int i = index + offset; i <= maxIndex; i += offset)
        {
            if (Squares[i].State != CurrentPlayer)
                break;
            connected++;
        }
        return connected;
    }


    private int FlattenIndex(int row, int col) => (row * 7) + col;

    #endregion
}
