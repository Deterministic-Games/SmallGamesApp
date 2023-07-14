using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallGamesApp.MVVMToolkit;

public class Player
{
	public List<Piece> Pieces { get; set; } = new();

	private List<NodeVM> HomeNodes { get; set; } = new();

	public bool HasWon()
	{
		return HomeNodes.All(node => node.Piece?.Owner == this);
	}
}
