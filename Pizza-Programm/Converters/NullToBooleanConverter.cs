// /Converters/NullToBooleanConverter.cs
using System;
using System.Globalization;
using System.Windows.Data;

namespace Pizza_Programm.Converters
{
    public class NullToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Wenn der Wert NICHT null ist, return true (Button ist Enabled)
            return value != null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}