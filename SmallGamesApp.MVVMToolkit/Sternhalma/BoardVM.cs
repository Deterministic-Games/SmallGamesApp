using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SmallGamesApp.MVVMToolkit.TicTacToe;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallGamesApp.MVVMToolkit;

public partial class BoardVM : ObservableObject
{
	public ObservableCollection<NodeVM> Nodes = new();

	public BoardVM()
	{
		BuildBoard();
	}

	[RelayCommand]
	public void Reset()
	{
		BuildBoard();
	}

	private void BuildBoard()
	{
		Nodes.Clear();
		// Add nodes representing the positions on the hop across board
		for (int i = 0; i < 6; i++)
		{
			for (int j = 0; j < 10; j++)
			{
				AddNode(i * 10 + j);
			}
		}

		// Define the edges for the hop across board
		// Each node is connected to its immediate neighbors
		// based on the hop across variation rules
		for (int i = 0; i < 6; i++)
		{
			for (int j = 0; j < 10; j++)
			{
				int nodeId = i * 10 + j;

				if (i > 0)
				{
					// Connect to the upper row
					AddEdge(nodeId, (i - 1) * 10 + j);
				}

				if (i < 5)
				{
					// Connect to the lower row
					AddEdge(nodeId, (i + 1) * 10 + j);
				}

				if (j > 0)
				{
					// Connect to the left node
					AddEdge(nodeId, i * 10 + (j - 1));
				}

				if (j < 9)
				{
					// Connect to the right node
					AddEdge(nodeId, i * 10 + (j + 1));
				}
			}
		}
	}

	private void AddNode(int id)
	{
		Nodes.Add(new NodeVM(id));
	}

	private void AddEdge(int nodeId1, int nodeId2)
	{
		if (TryGetNode(nodeId1, out var node1) && TryGetNode(nodeId2, out var node2))
		{
			node1!.adjacent.Add(node2!);
			node2!.adjacent.Add(node1!);
		}
	}

	private bool TryGetNode(int id, out NodeVM? node)
	{
		node = null;
		foreach (NodeVM n in Nodes)
		{
			if (n.Id == id)
			{
				node = n;
				return true;
			}
		}
		return false;
	}





	private readonly int[] boardLayout = new int[]
		{
			-1, -1, -1, -1, -1, -1,  2,  3,  4,  5,  6,  7,  8, -1, -1, -1, -1, -1, -1, -1,
			-1, -1, -1, -1, -1,  1,  2,  3,  4,  5,  6,  7,  8,  9, -1, -1, -1, -1, -1, -1,
			-1, -1, -1, -1,  0,  1,  2,  3,  4,  5,  6,  7,  8,  9, 10, -1, -1, -1, -1, -1,
			-1, -1, -1,  0,  1,  2,  3,  4,  5,  6,  7,  8,  9, 10, 11, 12, -1, -1, -1, -1,
			-1, -1,  0,  1,  2,  3,  4,  5,  6,  7,  8,  9, 10, 11, 12, 13, 14, -1, -1, -1,
			-1,  0,  1,  2,  3,  4,  5,  6,  7,  8,  9, 10, 11, 12, 13, 14, 15, 16, -1, -1,
			 0,  1,  2,  3,  4,  5,  6,  7,  8,  9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19,
			 1,  2,  3,  4,  5,  6,  7,  8,  9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20,
			 2,  3,  4,  5,  6,  7,  8,  9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21,
			 3,  4,  5,  6,  7,  8,  9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22,
			 4,  5,  6,  7,  8,  9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23
		};
}
