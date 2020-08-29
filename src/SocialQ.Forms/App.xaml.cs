using SocialQ.Queue;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SocialQ.Forms
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            var _ = new SocialQStartup();
            MainPage = SocialQStartup.NavigateToStart<QueuesViewModel>();
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
