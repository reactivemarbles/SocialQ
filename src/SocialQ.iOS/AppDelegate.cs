using System;
using System.Collections.Generic;
using System.Linq;

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
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            this.ShinyFinishedLaunching(new SocialQStartup());

            Rg.Plugins.Popup.Popup.Init();

            global::Xamarin.Forms.Forms.Init();
            XF.Material.iOS.Material.Init();
            Locator.CurrentMutable.RegisterLazySingleton<IPopupNavigation>(() => PopupNavigation.Instance);

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }

        public override void ReceivedRemoteNotification(UIApplication application, NSDictionary userInfo)
            => this.ShinyDidReceiveRemoteNotification(userInfo, null);

        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
            => this.ShinyDidReceiveRemoteNotification(userInfo, completionHandler);

        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
            => this.ShinyRegisteredForRemoteNotifications(deviceToken);

        public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
            => this.ShinyFailedToRegisterForRemoteNotifications(error);

        public override void PerformFetch(UIApplication application, Action<UIBackgroundFetchResult> completionHandler)
            => this.ShinyPerformFetch(completionHandler);

        public override void HandleEventsForBackgroundUrl(UIApplication application, string sessionIdentifier, Action completionHandler)
            => this.ShinyHandleEventsForBackgroundUrl(sessionIdentifier, completionHandler);
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
