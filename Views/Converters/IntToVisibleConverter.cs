using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace Converters
{
    public class IntToVisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is int) && !(value is bool))
                throw new Exception("int to visible converter: the value must be int or bool");
            int val = -1;

            // for not paid invoices.
            if(value is bool)
            {
                bool v = (bool)value;
                if (!v)
                    val = 1;
            }
            else
            {
                val = (int)value;
            }
            if (val > -1)
            {
                return Visibility.Visible;
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
