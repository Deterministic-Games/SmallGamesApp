﻿using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace SmallGamesApp.Memory;
public class PositionToColorConverter : MarkupExtension, IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        int i = (int)value;

        return i switch
        {
            0 => App.Current.FindResource("nord10"),
            1 => App.Current.FindResource("nord11"),
            2 => App.Current.FindResource("nord12"),
            3 => App.Current.FindResource("nord13"),
            4 => App.Current.FindResource("nord14"),
            5 => App.Current.FindResource("nord15"),
            _ => App.Current.FindResource("nord4")
        };
           
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    public override object ProvideValue(IServiceProvider serviceProvider) => this;
}
