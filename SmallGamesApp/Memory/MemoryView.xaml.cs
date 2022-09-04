using SmallGamesApp.MVVMToolkit.Memory;
using System.Windows.Controls;

namespace SmallGamesApp.Memory;
/// <summary>
/// Interaction logic for MemoryView.xaml
/// </summary>
public partial class MemoryView : UserControl
{
    private MemoryBoardVM _viewModel;
    public MemoryView()
    {
        InitializeComponent();
        _viewModel = new MemoryBoardVM();
        DataContext = _viewModel;
    }
}
