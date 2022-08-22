using System.Windows;
using System.Windows.Controls;
using SmallGamesApp.Minesweeper;

namespace SmallGamesApp;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public UserControl GameView { get; set; } = new MinesweeperView();

    public MainWindow()
    {
        InitializeComponent();
        DataContext = GameView;
    }
}
