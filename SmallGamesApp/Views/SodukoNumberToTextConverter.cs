using SmallGamesApp.MVVMToolkit;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace SmallGamesApp;

internal sealed class SodukoNumberToTextConverter : MarkupExtension, IValueConverter
{
    public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (SodukoNumber)value switch
        {
            SodukoNumber.One => 1,
            SodukoNumber.Two => 2,
            SodukoNumber.Three => 3,
            SodukoNumber.Four => 4,
            SodukoNumber.Five => 5,
            SodukoNumber.Six => 6,
            SodukoNumber.Seven => 7,
            SodukoNumber.Eight => 8,
            SodukoNumber.Nine => 9,
            _ => ""
        };
    } 

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var str = (string)value;
        var val = int.Parse(str);
        return (SodukoNumber)(1 << (val - 1));
    }

    public override object ProvideValue(IServiceProvider serviceProvider) => this;
}
