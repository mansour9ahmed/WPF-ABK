using System;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace Converters
{
    public class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is bool)
            {
                if ((bool)value)
                    return new SolidColorBrush(Colors.DarkGreen);

                return new SolidColorBrush(Colors.Red);
            }

            return new SolidColorBrush(Colors.Black);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
