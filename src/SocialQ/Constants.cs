using System;

namespace SocialQ
{
    public static class Constants
    {
        public static TimeSpan DefaultCacheExpirationTimeOut => TimeSpan.FromHours(1);

        public static TimeSpan DefaultRequestTimeout  => TimeSpan.FromSeconds(15);

        public static TimeSpan DefaultDelay  => TimeSpan.FromSeconds(6);
    }
}