using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Data;

namespace UI.Converters
{
    public class LogTypedToBackgroundColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            switch((UI.Classes.LogTypesEnum)value)
            {
                case Classes.LogTypesEnum.Error:
                    return System.Windows.Media.Brushes.Red;
                case Classes.LogTypesEnum.Information:
                    return System.Windows.Media.Brushes.Blue;
                case Classes.LogTypesEnum.Receiving:
                    return System.Windows.Media.Brushes.Green;
                case Classes.LogTypesEnum.SendingText:
                    return System.Windows.Media.Brushes.Orange;
                case Classes.LogTypesEnum.SendingHex:
                    return System.Windows.Media.Brushes.OrangeRed;
                case Classes.LogTypesEnum.SendingPing:
                    return System.Windows.Media.Brushes.Violet;
                case Classes.LogTypesEnum.Buffer:
                    return System.Windows.Media.Brushes.Cyan;
                case Classes.LogTypesEnum.Other:

                default:
                    return System.Windows.Media.Brushes.Black;

            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
