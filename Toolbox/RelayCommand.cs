using System;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;

namespace Toolbox;

public class RelayCommand : ICommand
{
    [Required]
    private readonly Action _action;
    [Required]
    private readonly Func<bool> _predicate;

    public RelayCommand(Action action)
    {
        _action = action;
        _predicate = () => true;
    }

    public RelayCommand(Action action, Func<bool> predicate)
    {
        _action = action;
        _predicate = predicate;
    }

    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public bool CanExecute(object? parameter)
    {
        return _predicate();
    }

    public void Execute(object? parameter)
    {
        _action();
    }
}
