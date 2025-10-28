// /Converters/NullToBooleanConverter.cs
using System;
using System.Globalization;
using System.Windows.Data;

// CORRECTED: Namespace added
namespace Pizza_Programm.Converters
{
    public class NullToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // If the value is NOT null, return true (Button is Enabled)
            return value != null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}