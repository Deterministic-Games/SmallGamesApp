using System;
using System.Windows.Input;
using Minesweeper.ViewModels;

namespace Minesweeper.Commands
{
    internal class OpenSquareCommand : ICommand
    {
        private readonly SquareViewModel _viewModel;

        public OpenSquareCommand(SquareViewModel viewModel) => _viewModel = viewModel;

        #region ICommand implementation
        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object? parameter) => _viewModel.CanOpen;

        public void Execute(object? parameter) => _viewModel.Open();
        #endregion
    }
}