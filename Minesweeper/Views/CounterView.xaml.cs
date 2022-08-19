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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Minesweeper.Views;
/// <summary>
/// Interaction logic for CounterView.xaml
/// </summary>
public partial class CounterView : UserControl
{
    public int Counter
    {
        get => (int)GetValue(CounterProperty);
        set => SetValue(CounterProperty, value);
    }
    public static readonly DependencyProperty CounterProperty =
        DependencyProperty.Register(nameof(Counter), typeof(int), typeof(CounterView), new PropertyMetadata(0));

    public CounterView()
    {
        InitializeComponent();
    }
}