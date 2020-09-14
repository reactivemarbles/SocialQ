using SocialQ.Splash;
using Xamarin.Forms;

namespace SocialQ.Forms
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            var _ = new SocialQStartup();
            MainPage = SocialQStartup.NavigateToStart<SplashViewModel>();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
