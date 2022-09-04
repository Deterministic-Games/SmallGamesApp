using SmallGamesApp.Core;
using SmallGamesApp.Core.Minesweeper;
using System.Collections.ObjectModel;
using System.Windows.Input;
using SmallGamesApp.MVVMToolkit.TicTacToe;
using SmallGamesApp.MVVMToolkit.ConnectFour;
using SmallGamesApp.MVVMToolkit.Memory;

namespace SmallGamesApp;

public class MainViewModel : BaseViewModel
{
    #region Properties

    public ObservableCollection<object> ViewModels { get; set; }

    private object? _currentView;
    public object CurrentView
    {
        get => _currentView!;
        set
        {
            _currentView = value;
            OnPropertyChanged();
        }
    }

    #endregion

    #region Commands

    private ICommand? _changeGameCommand;
    public ICommand ChangeGameCommand => _changeGameCommand ??= new ParameterizedRelayCommand(ChangeGame);

    #endregion

    #region Constructor

    public MainViewModel()
    {
        ViewModels = new()
        {
            new MinesweeperBoardViewModel(),
            new TicTacToeBoardViewModel(),
            new ConnectFourBoardVM(),
            new MemoryBoardVM()
        };
        CurrentView = ViewModels[0];
    }

    #endregion

    public void ChangeGame(object obj)
    {
        var index = int.Parse((string)obj);

        if (index < ViewModels.Count) CurrentView = ViewModels[index];
    }
}
