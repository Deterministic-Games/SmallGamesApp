using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Toolbox
{
    public class ParameterizedRelayCommand : ICommand
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
            _action(parameter);
        }
    }
}