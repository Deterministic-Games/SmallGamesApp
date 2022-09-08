using SmallGamesApp.MVVMToolkit;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace SmallGamesApp;
internal class StateToColorConverter : MarkupExtension, IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var state = (SquareState)value;

        return state switch
        {
            SquareState.Player1 => App.Current.FindResource("nord11"),
            SquareState.Player2 => App.Current.FindResource("nord13"),
            _ => App.Current.FindResource("nord0")
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();

    public override object ProvideValue(IServiceProvider serviceProvider) => this;
}
