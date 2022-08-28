using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace SmallGamesApp.MVVMToolkit.ConnectFour;
public class ConnectFourBoardVM : ObservableObject
{
	public ObservableCollection<ConnectFourSquareVM> Squares { get; set; } = new();

	public ConnectFourBoardVM()
	{
		for (int col = 0; col < 7; col++)
		{
			var column = new ConnectFourSquareVM[]
            {
				new(0),
				new(1),
				new(2),
				new(3),
                new(4),
                new(5),
            };

            for (int row = 0; row < 6; row++)
			{
				var square = column[row];
                square.Column = column;
				Squares.Add(square);
			}
		}
	}
}
