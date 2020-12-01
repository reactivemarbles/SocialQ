using System;
using System.Globalization;
using Xamarin.Forms;

namespace SocialQ.Forms.Converters
{
    /// <summary>
    /// <see cref="IValueConverter"/> that handles date time to string conversion.
    /// </summary>
    public class DateTimeConverter : IValueConverter
    {
        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime)
            {
                return $"{value:hh:mm:ss}";
            }

            return string.Empty;
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => null!;
    }
}