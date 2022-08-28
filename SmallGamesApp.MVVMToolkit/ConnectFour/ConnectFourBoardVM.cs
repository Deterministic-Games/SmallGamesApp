using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace SmallGamesApp.MVVMToolkit.ConnectFour;
public partial class ConnectFourBoardVM : ObservableObject
{
    #region Properties

    public ObservableCollection<ConnectFourSquareVM> Squares { get; set; } = new();

	[ObservableProperty]
	private SquareState _currentPlayer = SquareState.Player1;

	[ObservableProperty]
	private SquareState _winner;

    #endregion

    #region Members

    private static readonly IDictionary<SquareState, SquareState> s_playerMap = new Dictionary<SquareState, SquareState>
	{
		{ SquareState.Player1, SquareState.Player2 },
		{ SquareState.Player2, SquareState.Player1 }
	};

    #endregion

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
	}

    #endregion

    [RelayCommand]
	private void PlaceToken(ConnectFourSquareVM sqr)
	{
		int col = sqr.Column;

		for (int r = 5; r >= 0; r--)
		{
			var square = Squares[FlattenIndex(r, col)];

            if (square.IsAvailable)
			{
				square.State = _currentPlayer;
                CheckForWin(square);

                CurrentPlayer = s_playerMap[_currentPlayer];
				break;
			}
		}
	}

    #region Private methods

    private void CheckForWin(ConnectFourSquareVM nSquare)
	{
		int row = nSquare.Row;
		int col = nSquare.Column;

		// Check row
		if (CheckRowForWin(row, col))
			Debug.WriteLine("Row win");

		// Check column if new square placed in top 3 rows
		if (row <= 2)
		{
			if (CheckColumnForWin(row, col))
                Debug.WriteLine("Col win");
        }

		// Check diagonals
		if (CheckDiagonals(row, col))
			Debug.WriteLine("Diag win");
    }

	private bool CheckColumnForWin(int startRow, int startCol)
	{
		int StartIndex = FlattenIndex(startRow + 1, startCol);
		int maxIndex = FlattenIndex(startRow + 3, startCol);

		for (int i = StartIndex; i <= maxIndex; i += 7)
		{
			if (Squares[i].State != _currentPlayer) return false;
		}
        return true;
	}

	private bool CheckRowForWin(int startRow, int startCol)
	{
		int connected = 0;

		int index = FlattenIndex(startRow, startCol);

        int minColumn = Math.Max(0, startCol - 3);
        int startIndex = FlattenIndex(startRow, minColumn);

        int maxColumn = Math.Min(6, startCol + 3);
        int endIndex = FlattenIndex(startRow, maxColumn);

        for (int i = startIndex; i <= endIndex; i++)
		{
            if (i == index)
                continue;

            connected = Squares[i].State == _currentPlayer ? connected + 1 : 0;

            if (connected >= 3)
				return true;
        }
		return false;
    }

	// Checks from upper left to lower right and from lower left to upper right
	private bool CheckDiagonals(int startRow, int startCol)
	{
        int index = FlattenIndex(startRow, startCol);

		// This diagonal "/"
		int maxDownOffset = Math.Min(Math.Abs(startRow + 3), Math.Abs(startCol - 3)) * 6;

		return false;

        /*int minRow = Math.Max(0, startRow - 3);
        int maxRow = Math.Min(5, startRow + 3);

		int minCol = Math.Max(0, startCol - 3);
		int maxCol = Math.Min(6, startCol + 3);

		int connectedFromUp = 0;
		int connectedFromDown = 0;

		int rowFromUp = minRow;
        int rowFromDown = maxRow;

        for (int col = minCol; col <= maxCol; col++)
		{
			if (col == startCol)
				continue;

			var fromUp = Squares[FlattenIndex(rowFromUp, col)];
			var fromDown = Squares[FlattenIndex(rowFromDown, col)];

            connectedFromUp = fromUp.State == _currentPlayer ? connectedFromUp + 1 : 0;
            connectedFromDown = fromDown.State == _currentPlayer ? connectedFromDown + 1 : 0;

			if (connectedFromUp >= 3 || connectedFromDown >= 3)
				return true;

			if (rowFromUp > minRow)
				rowFromUp--;
			if (rowFromDown < maxRow)
				rowFromDown++;
        }
		return false;*/
    }


    private int FlattenIndex(int row, int col) => (row * 7) + col;

    #endregion
}
