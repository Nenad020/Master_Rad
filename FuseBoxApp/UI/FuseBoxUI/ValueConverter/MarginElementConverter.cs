using System;
using System.Globalization;
using System.Windows;

namespace FuseBoxUI.ValueConverter
{
	public class MarginElementConverter : BaseValueConverter<MarginElementConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double[] vs = (double[])value;

            return new Thickness(vs[0], vs[1], vs[2], vs[3]);
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
