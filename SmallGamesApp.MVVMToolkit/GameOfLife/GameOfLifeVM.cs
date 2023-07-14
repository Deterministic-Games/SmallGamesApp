using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using System.Timers;

namespace SmallGamesApp.MVVMToolkit;

public partial class GameOfLifeVM : ObservableObject
{
	private readonly int _height;
	private readonly int _width;


	private bool[,] _cells;
	public bool[] Cells 
	{
		get => FlattenCells(_cells);
	}

	private readonly System.Timers.Timer _timer = new(500);

	public GameOfLifeVM()
	{
		_height = 32;
		_width = 32;
		_cells = new bool[_height, _width];

		_timer.Elapsed += HandleCycle;
		GenerateRandomState();
	}
	

	[RelayCommand]
	private void Start()
	{
		_timer.Start();
	}

	[RelayCommand]
	private void Stop()
	{
		_timer.Stop();
	}

	[RelayCommand]
	private void Reset()
	{
		_timer.Stop();

		GenerateRandomState();

		_timer.Start();
	}

	private void HandleCycle(object? sender, ElapsedEventArgs e)
	{
		var newCells = (bool[,])_cells.Clone();

		for (int i = 0; i < _height; i++)
		{
			for (int j = 0; j < _width; j++)
			{
				var neighbors = GetNrOfNeighbors(_cells, i, j);

				// Apply the rules of the Game of Life
				if (_cells[i, j] && (neighbors == 2 || neighbors == 3))
				{
					newCells[i, j] = true; // Cell survives
				}
				else if (!_cells[i, j] && neighbors == 3)
				{
					newCells[i, j] = true; // Cell spawns
				}
				else
				{
					newCells[i, j] = false; // Cell dies
				}
			}
		}
		UpdateCells(newCells);
	}

	private int GetNrOfNeighbors(bool[,] cells, int row, int col)
	{
		var neighbors = 0;
		for (int r = row - 1; r <= row + 1; r++)
		{
			for (int c = col - 1; c <= col + 1; c++)
			{
				if (r == row && c == col)
				{
					continue;
				}
				if (r >= 0 && r < _height && c >= 0 && c < _width)
				{
					if (cells[r, c])
					{
						neighbors++;
					}
				}
			}
		}
		return neighbors;
	}

	private void GenerateRandomState()
	{
		var random = new Random();
		var newCells = new bool[_height, _width];

		for (int row = 0; row < _height; row++)
		{
			for (int col = 0; col < _width; col++)
			{
				newCells[row, col] = random.NextDouble() < 0.5; // Assign a random boolean value
			}
		}
		UpdateCells(newCells);
	}

	private void UpdateCells(bool[,] newCells)
	{
		_cells = newCells;
		OnPropertyChanged(nameof(Cells));
	}

	private bool[] FlattenCells(bool[,] cells)
	{
		var flattenedCells = new bool[32 * 32];
		for (int row = 0; row < _height; row++)
		{
			for (int col = 0; col < _width; col++)
			{
				flattenedCells[row * _width + col] = cells[row, col];
			}
		}
		return flattenedCells;
	}
}
