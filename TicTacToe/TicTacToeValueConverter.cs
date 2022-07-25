using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using TicTacToe.Models;

namespace TicTacToe
{
    public class TicTacToeValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var squares = (Square[,])values[0];

                var colIndex = (int)values[1];
                var rowIndex = (int)values[2];

                var square = squares[colIndex, rowIndex];

                if (square.Player is null) return "";

                return square.Player;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
