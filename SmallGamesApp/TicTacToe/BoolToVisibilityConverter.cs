using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SmallGamesApp.TicTacToe;
internal class BoolToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var boolean = (bool)value;

        return boolean switch
        {
            true => Visibility.Visible,
            false => Visibility.Hidden
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
}
