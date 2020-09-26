using System;

namespace SocialQ
{
    public class TimeSpans
    {
        public static TimeSpan DefaultTextChanged => TimeSpan.FromMilliseconds(750);
        
        public static TimeSpan DefaultCacheExpirationTimeOut => TimeSpan.FromHours(1);

        public static TimeSpan DefaultRequestTimeout  => TimeSpan.FromSeconds(15);

        public static TimeSpan DefaultDelay  => TimeSpan.FromSeconds(6);
    }
}