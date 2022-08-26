using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace SmallGamesApp.MVVMToolkit.ConnectFour;
public class ConnectFourBoardVM : ObservableObject
{
    public ObservableCollection<ConnectFourColumnVM> Columns { get; set; } = new();

	public ObservableCollection<ConnectFourSquareVM> Squares { get; set; } = new();

	public ConnectFourBoardVM()
	{
		for (int col = 0; col < 7; col++)
		{
            Columns.Add(new ConnectFourColumnVM());
        }

		for (int row = 0; row < 6; row++)
		{
			for (int col = 0; col < 7; col++)
			{
				Columns[col].AddFirst();
				Squares.Add(Columns[col].First!);
			}
		}
	}
}
