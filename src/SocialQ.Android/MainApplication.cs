using System;
using Android.App;
using Android.Runtime;
using Shiny;
using SocialQ.Forms;

namespace SocialQ.Droid
{
    [Application]
    public class MainApplication : ShinyAndroidApplication<SocialQStartup>
    {
        public MainApplication(IntPtr handle, JniHandleOwnership transfer) : base(handle, transfer)
        {
        }


        public override void OnCreate()
        {
            base.OnCreate();
            Xamarin.Essentials.Platform.Init(this);
        }
    }
}