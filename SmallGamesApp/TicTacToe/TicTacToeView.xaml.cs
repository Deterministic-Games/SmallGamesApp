using SmallGamesApp.MVVMToolkit;
using System.Windows.Controls;

namespace SmallGamesApp;

public partial class TicTacToeView : UserControl
{
    private TicTacToeBoardVM _viewModel;

    public TicTacToeView()
    {
        InitializeComponent();
        _viewModel = new TicTacToeBoardVM();
        DataContext = _viewModel;
    }
}
