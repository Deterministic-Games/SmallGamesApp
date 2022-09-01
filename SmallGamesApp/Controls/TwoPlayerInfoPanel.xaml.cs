using SmallGamesApp.MVVMToolkit;
using SmallGamesApp.MVVMToolkit.ConnectFour;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SmallGamesApp.Controls;
/// <summary>
/// Interaction logic for TwoPlayerInfoPanel.xaml
/// </summary>
public partial class TwoPlayerInfoPanel : UserControl
{
    public SquareState CurrentPlayer
    {
        get => (SquareState)GetValue(CurrentPlayerProperty);
        set => SetValue(CurrentPlayerProperty, value);
    }
    public static readonly DependencyProperty CurrentPlayerProperty =
        DependencyProperty.Register("CurrentPlayer", typeof(SquareState), typeof(TwoPlayerInfoPanel));

    public SquareState Winner
    {
        get => (SquareState)GetValue(WinnerProperty);
        set => SetValue(WinnerProperty, value);
    }
    public static readonly DependencyProperty WinnerProperty =
        DependencyProperty.Register("Winner", typeof(SquareState), typeof(TwoPlayerInfoPanel));

    public bool IsGameOver
    {
        get => (bool)GetValue(IsGameOverProperty);
        set => SetValue(IsGameOverProperty, value);
    }
    public static readonly DependencyProperty IsGameOverProperty =
        DependencyProperty.Register("IsGameOver", typeof(bool), typeof(TwoPlayerInfoPanel), new PropertyMetadata(false));

    public ICommand RestartCommand
    {
        get => (ICommand)GetValue(RestartCommandProperty);
        set => SetValue(RestartCommandProperty, value);
    }
    public static readonly DependencyProperty RestartCommandProperty =
        DependencyProperty.Register("RestartCommand", typeof(ICommand), typeof(TwoPlayerInfoPanel));

    public TwoPlayerInfoPanel()
    {
        InitializeComponent();
    }
}
