using SocialQ.Startup;
using Xamarin.Forms;

namespace SocialQ.Forms
{
    /// <summary>
    /// Represents the xamarin forms application.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        public App()
        {
            InitializeComponent();
            XF.Material.Forms.Material.Init(this);
            var startup = new SocialQStartup();
            MainPage = SocialQStartup.NavigateToStart<SplashViewModel>();
        }

        /// <summary>
        /// Gets or sets the parent window or activity.
        /// </summary>
        public static object? ParentWindow { get; set; }

        /// <inheritdoc/>
        protected override void OnStart()
        {
        }

        /// <inheritdoc/>
        protected override void OnSleep()
        {
        }

        /// <inheritdoc/>
        protected override void OnResume()
        {
        }
    }
}
