using System;
using System.Windows.Input;
using Minesweeper.ViewModels;

namespace Minesweeper.Commands
{
    internal class FlagSquareCommand : ICommand
    {
        private readonly SquareViewModel _viewModel;

        public FlagSquareCommand(SquareViewModel viewModel) => _viewModel = viewModel;

        #region ICommand implementation
        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object? parameter) => _viewModel.CanFlag;

        public void Execute(object? parameter) => _viewModel.ToggleFlag();
        #endregion
    }
}