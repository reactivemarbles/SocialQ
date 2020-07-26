using SocialQ.ViewModels;
using Xamarin.Forms;

namespace SocialQ.Forms
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            var _ = new SocialQStartup();
            MainPage = SocialQStartup.NavigateToStart<MainViewModel>();
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
