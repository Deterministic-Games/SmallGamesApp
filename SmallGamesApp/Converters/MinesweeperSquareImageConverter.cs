using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media.Imaging;

namespace SmallGamesApp;
public class MinesweeperSquareImageConverter : IValueConverter
{
    public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var prefix = "pack://application:,,,/SmallGamesApp;component/Resources";

        if (value is string imageString)
        {
            Debug.WriteLine(imageString);
            return imageString switch
            {
                "M" => new BitmapImage(new Uri($"{prefix}/mine.png")),
                "F" => new BitmapImage(new Uri($"{prefix}/red-flag.png")),
                " " => null,
                _ => new BitmapImage(new Uri($"{prefix}/{imageString}.png"))
            };
        }
        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
}
