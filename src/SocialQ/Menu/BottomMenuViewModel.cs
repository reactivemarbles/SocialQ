using System;
using System.Collections.Generic;
using ReactiveUI;
using Sextant;
using Sextant.Plugins.Popup;
using SocialQ.Profile;
using SocialQ.Queue;
using SocialQ.Stores;
using Splat;

namespace SocialQ
{
    public class BottomMenuViewModel : ViewModelBase
    {
        private List<Func<IPopupViewStackService, TabViewModel>> _tabViewModels;

        public BottomMenuViewModel(IPopupViewStackService popupViewStackService)
            : base(popupViewStackService)
        {
            TabViewModels = new List<Func<IPopupViewStackService, TabViewModel>>
            {
                service => new TabViewModel("Search", "", service, () => CreateMenuItem<StoreSearchViewModel>(service)),
                service => new TabViewModel("Queue", "", service, () => CreateMenuItem<QueuesViewModel>(service)),
                service => new TabViewModel("Me", "", service, () => CreateMenuItem<UserViewModel>(service))
            };
        }

        private static ViewModelBase CreateMenuItem<TViewModel>(IPopupViewStackService service)
            where TViewModel : ViewModelBase
        {
            var viewmodel = Locator.Current.GetService<TViewModel>();
            viewmodel.SetNavigationService(service);
            return viewmodel;
        }

        public List<Func<IPopupViewStackService, TabViewModel>> TabViewModels
        {
            get => _tabViewModels;
            set => this.RaiseAndSetIfChanged(ref _tabViewModels, value);
        }
    }
}