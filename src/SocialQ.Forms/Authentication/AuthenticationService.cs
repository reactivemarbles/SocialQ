using System;
using System.Threading;
using System.Threading.Tasks;
using ReactiveUI;
using SocialQ.Forms;
using Splat;
using Xamarin.Forms.Auth;

namespace SocialQ.Authentication
{
    /// <summary>
    /// Represent an authentication service.
    /// </summary>
    public class AuthenticationService : ReactiveObject
    {
        // assumes you have made a class called AuthenticationConfig
        private static readonly IPublicClientApplication AuthenticationClient = PublicClientApplicationBuilder
            .Create(AuthenticationConfig.ClientId)
            .WithAuthority(AuthenticationConfig.Authority)
            .WithRedirectUri(AuthenticationConfig.RedirectUrl)
            .WithExtraQueryParameters(AuthenticationConfig.AdditionalQueryHeaders)
            .Build();

        /// <summary>
        /// Gets an access token.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The access token.</returns>
        public async Task<string> GetAccessToken(CancellationToken cancellationToken = default)
        {
            AuthenticationResult authResult;

            // let's see if we have the user details already available.
            try
            {
                authResult = await AuthenticationClient.AcquireTokenSilent(AuthenticationConfig.Scopes).ExecuteAsync(cancellationToken).ConfigureAwait(false);
            }
            catch (AuthUiRequiredException)
            {
                try
                {
                    authResult = await AuthenticationClient.AcquireTokenInteractive(AuthenticationConfig.Scopes)
                        .WithParentActivityOrWindow(App.ParentWindow)
                        .ExecuteAsync(cancellationToken)
                        .ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    this.Log().Warn(ex, "Could not log into the authentication system");
                    return null!;
                }
            }

            return authResult.AccessToken;
        }
    }
}