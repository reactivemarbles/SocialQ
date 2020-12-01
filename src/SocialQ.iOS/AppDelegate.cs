using System;
using Foundation;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Services;
using Shiny;
using SocialQ.Forms;
using Splat;
using UIKit;
using Xamarin.Forms.Auth;

namespace SocialQ.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the.
    // User Interface of the application, as well as listening (and optionally responding) to.
    // application events from iOS.

    /// <summary>
    /// Represents the ios application.
    /// </summary>
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        /// <inheritdoc/>
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            this.ShinyFinishedLaunching(new SocialQStartup());

            Rg.Plugins.Popup.Popup.Init();

            global::Xamarin.Forms.Forms.Init();
            XF.Material.iOS.Material.Init();

            Aurora.ComponentLoader.Init("hB3/dJoOX+oKUsAISq+HedynauXfjtIs7vlQFItEVfYmP0zDwOy71jao+fCo8mIfEPZ8Oecs0oK0VCiIN07ysCauJQY3vy4LcGu27hulZaFSrSgyqqSBI8eUhloFuUm/DtqebdFh8Ny/TEpaCz9XOTDaFo5Wt8EAz9XA+GWEnY8=");
            Locator.CurrentMutable.RegisterLazySingleton<IPopupNavigation>(() => PopupNavigation.Instance);

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }

        /// <inheritdoc/>
        public override void ReceivedRemoteNotification(UIApplication application, NSDictionary userInfo)
            => this.ShinyDidReceiveRemoteNotification(userInfo, null!);

        /// <inheritdoc/>
        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
            => this.ShinyDidReceiveRemoteNotification(userInfo, completionHandler);

        /// <inheritdoc/>
        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
            => this.ShinyRegisteredForRemoteNotifications(deviceToken);

        /// <inheritdoc/>
        public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
            => this.ShinyFailedToRegisterForRemoteNotifications(error);

        /// <inheritdoc/>
        public override void PerformFetch(UIApplication application, Action<UIBackgroundFetchResult> completionHandler)
            => this.ShinyPerformFetch(completionHandler);

        /// <inheritdoc/>
        public override void HandleEventsForBackgroundUrl(UIApplication application, string sessionIdentifier, Action completionHandler)
            => this.ShinyHandleEventsForBackgroundUrl(sessionIdentifier, completionHandler);

        /// <inheritdoc/>
        public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
        {
            var builder = PublicClientApplicationBuilder
                .Create("ClientId")
                .WithIosKeychainSecurityGroup("com.myapp.rules")
                .Build();
            return true;
        }
    }
}
