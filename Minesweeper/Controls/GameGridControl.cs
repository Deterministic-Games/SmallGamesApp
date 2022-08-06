using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations;
using System.Windows.Markup;

namespace Minesweeper.Controls;

public class GameGridControl : Grid, INotifyPropertyChanged
{
    [Required]
    [Range(1, 64, ErrorMessage = "Number of columns must be between {1} and {64}")]
    private int _numberOfColumns = 1;
    public int NumberOfColumns 
    { 
        get { return _numberOfColumns; }
        set
        {
            if (!Enumerable.Range(1, 64).Contains(value)) return;

            _numberOfColumns = value;
            SetupGrid();
        }
    }
    [Required]
    [Range(1, 64, ErrorMessage = "Number of rows must be between {1} and {64}")]
    private int _numberOfRows = 1;
    public int NumberOfRows 
    { 
        get { return _numberOfRows; } 
        set
        {
            if (!Enumerable.Range(1, 64).Contains(value)) return;

            _numberOfRows = value;
            SetupGrid();
        }
    }

    public GameGridControl() : base()
    {
        ShowGridLines = true;
    }

    void SetupGrid()
    {
        ColumnDefinitions.Clear();
        RowDefinitions.Clear();
        for (int i = 0; i < NumberOfColumns; i++)
        {
            AddColumn();
        }
        for (int i = 0; i < NumberOfRows; i++)
        {
            AddRow();
        }

        for (int i = 0; i < NumberOfColumns; i++)
        {
            for (int j = 0; j < NumberOfRows; j++)
            {
                AddSquare(i, j);
            }
        }
    }

    void AddRow()
    {
        var definition = new RowDefinition()
        {
            Height = new GridLength(1, GridUnitType.Star)
        };
        RowDefinitions.Add(definition);
    }

    void AddColumn()
    {
        var definition = new ColumnDefinition
        {
            Width = new GridLength(1, GridUnitType.Star)
        };
        ColumnDefinitions.Add(definition);
    }

    void AddSquare(int col, int row)
    {
        var button = new SquareControl
        {
            Content = "Hi",
            FontSize = 16,
            FontWeight = FontWeights.Bold
        };
        SetColumn(button, col);
        SetRow(button, row);
        Children.Add(button);
    }

    #region Propety Changed logic
    public event PropertyChangedEventHandler? PropertyChanged;
    internal void OnPropertyChanged([CallerMemberName] string? name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        SetupGrid();
    }
    #endregion
}