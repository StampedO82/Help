using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace AuxiliaryFeature.GUI.Converter
{
    public class StringToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value is string)
            {
                if ((string)value == Visibility.Hidden.ToString())
                    return Visibility.Hidden;

                if ((string)value == Visibility.Collapsed.ToString())
                    return Visibility.Collapsed;

                if ((string)value == Visibility.Visible.ToString())
                    return Visibility.Visible;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
