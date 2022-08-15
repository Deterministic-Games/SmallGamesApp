using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Minesweeper.Models;
using Toolbox;

namespace Minesweeper.ViewModels;

public class MinesweeperViewModel : BaseViewModel
{
    public ObservableCollection<SquareViewModel> Squares { get; set; }

    #region Wrapper
    private MinesweeperModel _minesweeperModel = new();

    public int Width 
    { 
        get { return _minesweeperModel.Width; }
        set
        {
            _minesweeperModel.Width = value;
            OnPropertyChanged();
        }
    }
    public int Height 
    { 
        get { return _minesweeperModel.Height; }
        set
        {
            _minesweeperModel.Height = value;
            OnPropertyChanged();
        }
    }
    public int Mines 
    { 
        get { return _minesweeperModel.Mines; }
        set
        {
            _minesweeperModel.Mines = value;
            OnPropertyChanged();
        }
    }
    #endregion

    public MinesweeperViewModel()
    {
        Squares = new(_minesweeperModel.GetSquares().Select(sqr => new SquareViewModel(sqr)));

        #if DEBUG
        Debug.WriteLine(_minesweeperModel.ToString());
        #endif
    }
}