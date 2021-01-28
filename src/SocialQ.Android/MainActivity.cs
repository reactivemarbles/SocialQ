using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Services;
using Shiny;

using SocialQ.Forms;
using Splat;
using Xamarin.Forms.Auth;

namespace SocialQ.Droid
{
    [Activity(Label = "SocialQ.Forms", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        /// <inheritdoc/>
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            this.ShinyRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        /// <inheritdoc/>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            App.ParentWindow = this;
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Rg.Plugins.Popup.Popup.Init(this);
            Locator.CurrentMutable.RegisterLazySingleton<IPopupNavigation>(() => PopupNavigation.Instance);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            XF.Material.Droid.Material.Init(this, savedInstanceState);
            Aurora.ComponentLoader.Init("hB3/dJoOX+oKUsAISq+HedynauXfjtIs7vlQFItEVfYmP0zDwOy71jao+fCo8mIfEPZ8Oecs0oK0VCiIN07ysCauJQY3vy4LcGu27hulZaFSrSgyqqSBI8eUhloFuUm/DtqebdFh8Ny/TEpaCz9XOTDaFo5Wt8EAz9XA+GWEnY8=");
            LoadApplication(new App());

            this.ShinyOnCreate();
        }

        /// <inheritdoc/>
        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);
            this.ShinyOnNewIntent(intent);
        }

        /// <inheritdoc/>
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            AuthenticationContinuationHelper
                .SetAuthenticationContinuationEventArgs(requestCode, resultCode, data);
        }
    }
}