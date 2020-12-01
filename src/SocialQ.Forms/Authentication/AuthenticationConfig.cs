using System;
using System.Collections.Generic;

namespace SocialQ.Authentication
{
    internal static class AuthenticationConfig
    {
        /// <summary>
        /// Gets or sets the client id.
        /// </summary>
        public static string? ClientId { get; set; }

        /// <summary>
        /// Gets or sets the authority.
        /// </summary>
        public static Uri? Authority { get; set; }

        /// <summary>
        /// Gets or sets the scopes.
        /// </summary>
        public static IEnumerable<string>? Scopes { get; set; }

        /// <summary>
        /// Gets or sets the redirect uri.
        /// </summary>
        public static Uri? RedirectUrl { get; set; }

        /// <summary>
        /// Gets or sets the addition query headers.
        /// </summary>
        public static IDictionary<string, string>? AdditionalQueryHeaders { get; set; }
    }
}