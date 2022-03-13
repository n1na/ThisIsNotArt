using System;
using System.Globalization;
using System.Windows.Data;

namespace ThisIsNotArt.Converters
{
    public class ToUpperCaseConverter : IValueConverter
    {
        /// <summary>
		/// Converts string to uppercase
		/// </summary>
		/// <param name="value">The value produced by the binding source</param>
		/// <param name="targetType">The type of the binding target property</param>
		/// <param name="parameter">The converter parameter to use</param>
		/// <param name="culture">The culture to use in the converter</param>
        public object Convert(Object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((string)value).ToUpper();
        }

        /// <summary>
		/// We don't really need this ¯\_(ツ)_/¯
		/// </summary>
		/// <param name="value">The value that is produced by the binding target</param>
		/// <param name="targetType">The type to convert to</param>
		/// <param name="parameter">The converter parameter to use</param>
		/// <param name="culture">The culture to use in the converter</param>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
