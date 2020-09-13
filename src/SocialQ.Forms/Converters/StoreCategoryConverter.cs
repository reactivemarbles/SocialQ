using System;
using System.Globalization;
using Xamarin.Forms;

namespace SocialQ.Forms.Converters
{
    public class StoreCategoryConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is StoreCategory category)
            {
                switch (category)
                {
                    case StoreCategory.Restaurant:
                        return Color.Green;
                    case StoreCategory.Grocery:
                        return Color.Firebrick;
                    case StoreCategory.Sporting:
                        return Color.CornflowerBlue;
                    case StoreCategory.Laundry:
                        return Color.SeaGreen;
                    case StoreCategory.Furniture:
                        return Color.DarkOrchid;
                    case StoreCategory.HomeImprovement:
                        return Color.DarkOrange;
                    case StoreCategory.Gym:
                        return Color.Goldenrod;
                    case StoreCategory.Electronics:
                        return Color.CornflowerBlue;
                    default:
                        return Color.CornflowerBlue;
                }
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}