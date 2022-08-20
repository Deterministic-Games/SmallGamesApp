using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace SmallGamesApp;

public class BackgroundConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var isOpen = (bool)value;

        return isOpen ? new SolidColorBrush(Color.FromRgb(171, 162, 145)) : new SolidColorBrush(Color.FromRgb(28, 28, 28));
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
}