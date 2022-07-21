﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for TicTacToeWindow.xaml
    /// </summary>
    public partial class TicTacToeWindow : Window
    {
        public TicTacToe Game { get; private set; } = new();

        public TicTacToeWindow()
        {
            InitializeComponent();
            DataContext = Game;
        }

        private void TicTacToeSquare_Click(object sender, RoutedEventArgs e)
        {
            Game.CurrentTurn++;
        }
    }
}
