using SmallGamesApp.MVVMToolkit.ConnectFour;
using System.Windows.Controls;

namespace SmallGamesApp.ConnectFour;
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
