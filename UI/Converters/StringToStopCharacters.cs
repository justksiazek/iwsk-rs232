using System;
using System.Linq;
using System.Windows.Data;

namespace Task1.Converters
{
    public class StringToStopCharacters : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            var str = ( (string)value ).ToCharArray();
            string returnString = string.Empty;

            foreach(var item in str) {
                var it = (Int16)( (byte)item );
                returnString += it.ToString("X2") + ";";
            }

            return returnString;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            string returnString = string.Empty;
            try {
                ((string)value).Trim()
                               .Split(new char[] {';'}, StringSplitOptions.RemoveEmptyEntries)
                               .Select(x => (byte)System.Convert.ToInt32(x, 16)).ToList()
                               .ForEach(x => returnString += (char)x);
            }
            catch(Exception e) {
                returnString = "\n";
            }
            return returnString;
        }
    }
}
