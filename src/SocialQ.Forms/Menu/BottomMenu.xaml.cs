using System;
using ReactiveUI;
using ReactiveUI.XamForms;
using Rg.Plugins.Popup.Services;
using Sextant;
using Sextant.Plugins.Popup;
using Sextant.XamForms;
using Splat;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace SocialQ.Forms.Menu
{
    /// <summary>
    /// Represents the bottom menu <see cref="ReactiveTabbedPage{TViewModel}"/>.
    /// </summary>
    public partial class BottomMenu
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BottomMenu"/> class.
        /// </summary>
        public BottomMenu()
        {
            InitializeComponent();

            On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);

            NavigationPage.SetHasNavigationBar(this, false);

            this.WhenActivated(_ => ViewModel!.TabViewModels!.ForEach(x => Children.Add(CreateTab(x))));
        }

        private Page CreateTab(Func<IPopupViewStackService, TabViewModel> viewModelFunc)
        {
            var bgScheduler = RxApp.TaskpoolScheduler;
            var mScheduler = RxApp.MainThreadScheduler;
            var vLocator = Locator.Current.GetService<IViewLocator>();

            var navigationView = new NavigationView(mScheduler, bgScheduler, vLocator);
            var viewStackService = new PopupViewStackService(navigationView, PopupNavigation.Instance, vLocator, ViewModelFactory.Current);
            var model = viewModelFunc(viewStackService);

            navigationView.Title = model.TabTitle;
            navigationView.IconImageSource = ImageSource.FromFile(model.TabIcon);

            navigationView.PushPage(model.ViewModel, null, true, false).Subscribe();
            return navigationView;
        }
    }
}