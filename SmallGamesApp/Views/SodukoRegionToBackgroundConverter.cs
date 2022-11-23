using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Navigation;

namespace SmallGamesApp;
internal sealed class SodukoRegionToBackgroundConverter : MarkupExtension, IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        int region = (int)value;
        int mod = region % 2;

        return mod switch
        {
            0 => App.Current.FindResource("nord4"),
            _ => App.Current.FindResource("nord6")
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

    public override object ProvideValue(IServiceProvider serviceProvider) => this;
}
