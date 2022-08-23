using SmallGamesApp.MVVMToolkit.TicTacToe;
using System.Windows.Controls;

namespace SmallGamesApp.TicTacToe;
public partial class TicTacToeView : UserControl
{
    private TicTacToeBoardViewModel _viewModel;
    public TicTacToeView()
    {
        InitializeComponent();
        _viewModel = new TicTacToeBoardViewModel();
        DataContext = _viewModel;
    }
}
