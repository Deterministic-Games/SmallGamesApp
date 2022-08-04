using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Minesweeper.Controls;

public class SquareControl : Button
{
    new public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached("Command", typeof(ICommand), typeof(SquareControl));
    [Bindable(true)]
    [Category("Action")]
    new public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }


    public static readonly DependencyProperty Command2Property = DependencyProperty.RegisterAttached("Command2", typeof(ICommand), typeof(SquareControl));
    [Bindable(true)]
    [Category("Action")]
    public ICommand Command2 
    { 
        get => (ICommand)GetValue(Command2Property); 
        set => SetValue(Command2Property, value);
    }

    public SquareControl() : base() { }

    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        if (Command.CanExecute(null)) Command.Execute(null);
    }

    protected override void OnMouseRightButtonDown(MouseButtonEventArgs e)
    {
        if (Command2.CanExecute(null)) Command2.Execute(null);
    }

    protected override void OnClick() { }
}