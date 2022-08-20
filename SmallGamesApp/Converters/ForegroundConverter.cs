using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace SmallGamesApp;

public class ForegroundConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var isOpen = (bool)value;

        return isOpen ? new SolidColorBrush(Color.FromRgb(57, 56, 28)) : new SolidColorBrush(Color.FromRgb(255, 249, 225));
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
}