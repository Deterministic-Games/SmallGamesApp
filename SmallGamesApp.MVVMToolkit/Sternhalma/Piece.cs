using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallGamesApp.MVVMToolkit;

public class Piece
{
	public Player Owner { get; set; }

	public Piece(Player owner)
	{
		Owner = owner;
	}
}
