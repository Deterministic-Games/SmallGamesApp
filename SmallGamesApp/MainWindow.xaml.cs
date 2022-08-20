using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using SmallGamesApp.Controls;
using SmallGamesApp.Views;

namespace SmallGamesApp;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public UserControl GameView { get; set; } = new MinesweeperView();

    public MainWindow()
    {
        InitializeComponent();
        DataContext = GameView;
    }
}
