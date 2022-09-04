using System.Windows.Controls;
using SmallGamesApp.Core;

namespace SmallGamesApp;

/// <summary>
/// Interaction logic for MinesweeperView.xaml
/// </summary>
public partial class MinesweeperView : UserControl
{
    private MinesweeperBoardVM _viewModel;

    public MinesweeperView()
    {
        InitializeComponent();
        _viewModel = new MinesweeperBoardVM();
        DataContext = _viewModel;
    }
}
