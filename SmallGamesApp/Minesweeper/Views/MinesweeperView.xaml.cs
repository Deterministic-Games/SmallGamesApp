using System.Windows.Controls;
using SmallGamesApp.Core.Minesweeper;

namespace SmallGamesApp.Minesweeper;

/// <summary>
/// Interaction logic for MinesweeperView.xaml
/// </summary>
public partial class MinesweeperView : UserControl
{
    private MinesweeperBoardViewModel _viewModel;

    public MinesweeperView()
    {
        InitializeComponent();
        _viewModel = new MinesweeperBoardViewModel();
        DataContext = _viewModel;
    }
}
