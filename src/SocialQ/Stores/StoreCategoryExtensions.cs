using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialQ.Stores
{
    /// <summary>
    /// enum extensions.
    /// </summary>
    public static class StoreCategoryExtensions
    {
        /// <summary>
        /// Generates a list of values from a provided enum.
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <returns>The enumeration options.</returns>
        public static List<T> ListEnumeration<T>()
            where T : Enum
            => ((T[])Enum.GetValues(typeof(T))).ToList();
    }
}