using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Toolbox;

public partial class DynamicGridControl : UserControl
{
    private readonly GridLength PROPORTIONAL = new GridLength(1, GridUnitType.Star);

    public int NumberOfColumns
    {
        get { return (int)GetValue(NumberOfColumnsProperty); }
        set { SetValue(NumberOfColumnsProperty, value); }
    }
    public static readonly DependencyProperty NumberOfColumnsProperty =
        DependencyProperty.Register("NumberOfColumns", typeof(int), typeof(DynamicGridControl), new PropertyMetadata(1));


    public int NumberOfRows
    {
        get { return (int)GetValue(NumberOfRowsProperty); }
        set { SetValue(NumberOfRowsProperty, value); }
    }
    public static readonly DependencyProperty NumberOfRowsProperty =
        DependencyProperty.Register("NumberOfRows", typeof(int), typeof(DynamicGridControl), new PropertyMetadata(1));



    public IEnumerable ItemsSource
    {
        get { return (IEnumerable)GetValue(ItemsSourceProperty); }
        set { SetValue(ItemsSourceProperty, value); }
    }
    public static readonly DependencyProperty ItemsSourceProperty =
        DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(DynamicGridControl));

    public DynamicGridControl()
    {
        InitializeComponent();
    }

    private void UpdateGridDefinitions()
    {
        Grid.RowDefinitions.Clear();
        Grid.ColumnDefinitions.Clear();

        for (int row = 0; row < NumberOfRows; row++)
        {
            Grid.RowDefinitions.Add(new() { Height = PROPORTIONAL });
        }

        for (int col = 0; col < NumberOfColumns; col++)
        {
            Grid.ColumnDefinitions.Add(new() { Width = PROPORTIONAL });
        }
    }
}
