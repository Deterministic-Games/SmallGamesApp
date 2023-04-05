using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace SmallGamesApp;
public class CanInteractWithBoardConverter : MarkupExtension, IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.Length != 2 || values[0] is not bool isGameOver || values[1] is not bool isAITurn)
            return DependencyProperty.UnsetValue;

        return !isGameOver && !isAITurn;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

    public override object ProvideValue(IServiceProvider serviceProvider) => this;
}

