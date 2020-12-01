using System;
using System.Globalization;
using SocialQ.Stores;
using Xamarin.Forms;

namespace SocialQ.Forms.Converters
{
    /// <summary>
    /// <see cref="IValueConverter"/> that handles date time to store category conversion.
    /// </summary>
    public class StoreCategoryConverter : IValueConverter
    {
        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is StoreCategory category)
            {
                return category switch
                {
                    StoreCategory.Restaurant => Color.Green,
                    StoreCategory.Grocery => Color.Firebrick,
                    StoreCategory.Sporting => Color.CornflowerBlue,
                    StoreCategory.Laundry => Color.SeaGreen,
                    StoreCategory.Furniture => Color.DarkOrchid,
                    StoreCategory.HomeImprovement => Color.DarkOrange,
                    StoreCategory.Gym => Color.Goldenrod,
                    StoreCategory.Electronics => Color.DodgerBlue,
                    _ => Color.CornflowerBlue
                };
            }

            return null!;
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => null!;
    }
}