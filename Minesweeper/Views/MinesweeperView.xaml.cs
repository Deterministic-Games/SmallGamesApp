using System.Windows.Controls;
using Minesweeper.ViewModels;
using Toolbox;

namespace Minesweeper.Views
{
    /// <summary>
    /// Interaction logic for MinesweeperView.xaml
    /// </summary>
    public partial class MinesweeperView : UserControl
    {
        private MinesweeperViewModel _viewModel;

        public MinesweeperView()
        {
            InitializeComponent();
            _viewModel = new MinesweeperViewModel();
            DataContext = _viewModel;
        }
    }
}
