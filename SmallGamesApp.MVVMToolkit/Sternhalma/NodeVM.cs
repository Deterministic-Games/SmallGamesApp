using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallGamesApp.MVVMToolkit;

public partial class NodeVM : ObservableObject
{
	public int Id { get; set; }

	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(HasPiece))]
	private Piece? _piece;

	public bool HasPiece => _piece != null;

	public NodeVM(int id)
	{
		Id = id;
	}

	public readonly List<NodeVM> adjacent = new();
}

