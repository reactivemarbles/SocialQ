using System;
using System.Collections.Generic;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Sextant;
using Sextant.Plugins.Popup;
using SocialQ.Profile;
using SocialQ.Queue;
using SocialQ.Stores;
using Splat;

namespace SocialQ
{
    /// <summary>
    /// <see cref="ViewModelBase"/> that represents the bottom menu.
    /// </summary>
    public class BottomMenuViewModel : ViewModelBase
    {
        private List<Func<IPopupViewStackService, TabViewModel>>? _tabViewModels;

        /// <summary>
        /// Initializes a new instance of the <see cref="BottomMenuViewModel"/> class.
        /// </summary>
        /// <param name="popupViewStackService">The popup view stack service.</param>
        public BottomMenuViewModel(IPopupViewStackService popupViewStackService)
            : base(popupViewStackService) => TabViewModels = new List<Func<IPopupViewStackService, TabViewModel>>
        {
            service => new TabViewModel("Search", string.Empty, service, () => CreateMenuItem<StoreSearchViewModel>(service)),
            service => new TabViewModel("Queue", string.Empty, service, () => CreateMenuItem<QueuesViewModel>(service)),
            service => new TabViewModel("Me", string.Empty, service, () => CreateMenuItem<UserViewModel>(service))
        };

        /// <summary>
        /// Gets or sets the tab view models.
        /// </summary>
        [Reactive] public List<Func<IPopupViewStackService, TabViewModel>>? TabViewModels { get; set; }

        private static ViewModelBase CreateMenuItem<TViewModel>(IPopupViewStackService service)
            where TViewModel : ViewModelBase
        {
            var viewmodel = Locator.Current.GetService<TViewModel>();
            viewmodel.SetNavigationService(service);
            return viewmodel;
        }
}
}