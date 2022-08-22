using System.Windows;
using System.Windows.Controls;

namespace SmallGamesApp.Minesweeper;
/// <summary>
/// Interaction logic for CounterView.xaml
/// </summary>
public partial class CounterControl : UserControl
{
    public int Counter
    {
        get => (int)GetValue(CounterProperty);
        set => SetValue(CounterProperty, value);
    }
    public static readonly DependencyProperty CounterProperty =
        DependencyProperty.Register(nameof(Counter), typeof(int), typeof(CounterControl), new PropertyMetadata(0));

    public CounterControl()
    {
        InitializeComponent();
    }
}
