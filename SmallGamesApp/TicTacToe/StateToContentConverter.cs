using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using SmallGamesApp.MVVMToolkit;

namespace SmallGamesApp;

public class StateToContentConverter : MarkupExtension, IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is SquareState state)
        {
            return state switch
            {
                SquareState.Player1 => "X",
                SquareState.Player2 => "O",
                _ => " "
            };
        }
        return " ";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();

    public override object ProvideValue(IServiceProvider serviceProvider) => this;
}
