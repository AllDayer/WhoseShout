using MvvmCross.Platform.Converters;
using System;
using System.Globalization;

namespace WhoseShout
{
    public class InverseBoolConverter : IMvxValueConverter

    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !((bool)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !((bool)value);
        }
    }
}