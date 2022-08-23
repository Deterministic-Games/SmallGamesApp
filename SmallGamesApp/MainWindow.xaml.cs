using System.Windows;
using System.Windows.Controls;
using SmallGamesApp.Minesweeper;

namespace SmallGamesApp;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainViewModel();
    }
}
