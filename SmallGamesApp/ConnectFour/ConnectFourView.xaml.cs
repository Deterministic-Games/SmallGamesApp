using SmallGamesApp.MVVMToolkit;
using System.Windows.Controls;

namespace SmallGamesApp;
/// <summary>
/// Interaction logic for ConnectFourView.xaml
/// </summary>
public partial class ConnectFourView : UserControl
{
    private ConnectFourBoardVM _viewModel;

    public ConnectFourView()
    {
        InitializeComponent();
        _viewModel = new();
        DataContext = _viewModel;
    }
}
