using System;

namespace SocialQ
{
    /// <summary>
    /// The default time spans for various operations of the sub system.
    /// </summary>
    public class TimeSpans
    {
        /// <summary>
        /// Gets the default text change time.
        /// </summary>
        public static TimeSpan DefaultTextChanged => TimeSpan.FromMilliseconds(750);

        /// <summary>
        /// Gets the default cache expiration time out.
        /// </summary>
        public static TimeSpan DefaultCacheExpirationTimeOut => TimeSpan.FromHours(1);

        /// <summary>
        /// Gets the default timeout request.
        /// </summary>
        public static TimeSpan DefaultRequestTimeout => TimeSpan.FromSeconds(15);

        /// <summary>
        /// Gets the default delay time.
        /// </summary>
        public static TimeSpan DefaultDelay => TimeSpan.FromSeconds(6);
    }
}