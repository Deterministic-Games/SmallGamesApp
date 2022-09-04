using SmallGamesApp.Core;
using SmallGamesApp.MVVMToolkit;
using System.Collections.ObjectModel;
using System.Windows.Input;

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
            new MinesweeperBoardVM(),
            new TicTacToeBoardVM(),
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
