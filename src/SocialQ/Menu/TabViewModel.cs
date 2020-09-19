using System;
using Sextant;
using Xamarin.Forms;

namespace SocialQ
{
    public class TabViewModel : ViewModelBase
    {
        public TabViewModel(string tabTitle, string tabIcon, IParameterViewStackService stackService, Func<ViewModelBase> pageCreate)
            : base(stackService)
        {
            TabIcon = tabIcon;
            TabTitle = tabTitle;
            ViewModel = pageCreate();
        }

        public string TabTitle { get; }

        public FileImageSource TabIcon { get; }

        public ViewModelBase ViewModel { get; private set; }
    }
}