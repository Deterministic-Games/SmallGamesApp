using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace SmallGamesApp;

public class RestartImageConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        var prefix = "pack://application:,,,/SmallGamesApp;component/Minesweeper/Resources";
        var image = new BitmapImage(new Uri($"{prefix}/excited.png"));

        if (values[0] is bool hasWon && values[1] is bool hasLost)
        {
            if (hasWon)
                image = new BitmapImage(new Uri($"{prefix}/happy.png"));
            else if (hasLost)
                image = new BitmapImage(new Uri($"{prefix}/crying.png"));
        }
        return image;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => throw new NotImplementedException();
}