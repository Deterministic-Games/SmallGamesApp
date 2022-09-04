using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace SmallGamesApp;
public class BrushToColorConverter : MarkupExtension, IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is SolidColorBrush brush)
        {
            var color = brush.Color;

            return color;
        }
        else
        {
            throw new NotImplementedException();
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    public override object ProvideValue(IServiceProvider serviceProvider) => this;
}
