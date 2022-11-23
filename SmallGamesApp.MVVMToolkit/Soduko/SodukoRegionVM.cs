using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallGamesApp.MVVMToolkit;

public class SodukoRegionVM : ObservableObject
{
    public ObservableCollection<SodukoSquareVM> Squares { get; set; } = new();

	public int Region { get; init; }

	public SodukoRegionVM(int region)
	{
		for (int i = 0; i < 9; i++)
		{
			Squares.Add(new());
		}
		Region = region;
	}

	public bool IsComplete()
	{
		SodukoNumber nr = SodukoNumber.None;

		foreach (var square in Squares)
		{
			nr |= square.Number;
		}
		return nr == SodukoNumber.All;
	}
}
