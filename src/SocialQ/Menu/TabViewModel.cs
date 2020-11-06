using System;
using Sextant;
using Sextant.Plugins.Popup;

namespace SocialQ
{
    public class TabViewModel : ViewModelBase
    {
        public TabViewModel(string tabTitle, string tabIcon, IPopupViewStackService stackService, Func<ViewModelBase> pageCreate)
            : base(stackService)
        {
            TabIcon = tabIcon;
            TabTitle = tabTitle;
            ViewModel = pageCreate();
        }

        public string TabTitle { get; }

        public string TabIcon { get; }

        public ViewModelBase ViewModel { get; private set; }
    }
}