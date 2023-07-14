using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace SmallGamesApp;

public class BoolToColorConverter : MarkupExtension, IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		var isAlive = (bool)value;

		return isAlive ? App.Current.FindResource("nord14") : App.Current.FindResource("nord4");
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}

	public override object ProvideValue(IServiceProvider serviceProvider) => this;
}

