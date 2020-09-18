using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ReactiveUI;
using SocialQ.Forms;
using Splat;
using Xamarin.Forms.Auth;

namespace SocialQ.Authentication
{
    public class AuthenticationService : ReactiveObject
    {
        
        // assumes you have made a class called AuthenticationConfig
        private static readonly IPublicClientApplication AuthenticationClient = PublicClientApplicationBuilder
            .Create(AuthenticationConfig.ClientId)
            .WithAuthority(AuthenticationConfig.Authority)
            .WithRedirectUri(AuthenticationConfig.RedirectUrl)
            .WithExtraQueryParameters(AuthenticationConfig.AdditionalQueryHeaders)
            .Build();

        public AuthenticationService()
        {
        }

        public async Task<string> GetAccessToken(CancellationToken token = default)
        {
            AuthenticationResult authResult;

            // let's see if we have the user details already available.
            try
            {
                authResult = await AuthenticationClient.AcquireTokenSilent(AuthenticationConfig.Scopes).ExecuteAsync(token).ConfigureAwait(false);
            }
            catch (AuthUiRequiredException)
            {
                try
                {
                    authResult = await AuthenticationClient.AcquireTokenInteractive(AuthenticationConfig.Scopes)
                        .WithParentActivityOrWindow(App.ParentWindow)
                        .ExecuteAsync(token)
                        .ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    this.Log().Warn(ex, "Could not log into the authentication system");
                    return null;
                }
            }

            return authResult.AccessToken;
        }
    }

    internal class AuthenticationConfig
    {
        public static string ClientId { get; set; }
        public static Uri Authority { get; set; }
        public static IEnumerable<string> Scopes { get; set; }
        public static Uri RedirectUrl { get; set; }
        public static IDictionary<string, string> AdditionalQueryHeaders { get; set; }
    }
}