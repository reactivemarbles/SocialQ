using System;
using Sextant;
using Sextant.Plugins.Popup;

namespace SocialQ
{
    /// <summary>
    /// <see cref="ViewModelBase"/> that represents a tab.
    /// </summary>
    public class TabViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TabViewModel"/> class.
        /// </summary>
        /// <param name="tabTitle">The tab title.</param>
        /// <param name="tabIcon">The tab icon.</param>
        /// <param name="stackService">The stack service.</param>
        /// <param name="pageCreate">The page factory.</param>
        public TabViewModel(string tabTitle, string tabIcon, IPopupViewStackService stackService, Func<ViewModelBase> pageCreate)
            : base(stackService)
        {
            TabIcon = tabIcon;
            TabTitle = tabTitle;
            ViewModel = pageCreate();
        }

        /// <summary>
        /// Gets the tab title.
        /// </summary>
        public string TabTitle { get; }

        /// <summary>
        /// Gets the tab icon.
        /// </summary>
        public string TabIcon { get; }

        /// <summary>
        /// Gets the tab view model.
        /// </summary>
        public ViewModelBase ViewModel { get; }
    }
}