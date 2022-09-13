using System.ComponentModel.DataAnnotations;
using System.Windows.Input;

namespace SmallGamesApp.Core;

public sealed class ParameterizedRelayCommand : ICommand
{
    [Required]
    private readonly Action<object> _action;
    [Required]
    private readonly Func<bool> _predicate;

    public ParameterizedRelayCommand(Action<object> action)
    {
        _action = action;
        _predicate = () => true;
    }

    public ParameterizedRelayCommand(Action<object> action, Func<bool> predicate)
    {
        _action = action;
        _predicate= predicate;
    }

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter) => _predicate();

    public void Execute(object? parameter) => _action(parameter);
}