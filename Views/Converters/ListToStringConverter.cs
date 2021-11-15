using Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Documents;

namespace Converters
{
    public class ListToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string txt = "";
            if (value == null)
                return "";

            if(value is List<string>)
            {
                List<string> list = (List<string>)value;
                if (list.Count == 0)
                    return "";
                txt += list[0];
                foreach(var item in list.Skip(1))
                {
                    txt += ", "+item;
                }

                return txt;
            }

            return "Convertion ERROR..";

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string))
                throw new ArgumentException();

            string emails = (string)value;

            return string.Concat(emails.Where(c => !char.IsWhiteSpace(c))).Split(',').ToList();
        }
    }
}
