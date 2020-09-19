using System;
using System.Collections.Generic;
using ReactiveUI;
using Sextant;
using SocialQ.Queue;
using Splat;

namespace SocialQ
{
    public class BottomMenuViewModel : ViewModelBase
    {
        private List<Func<IParameterViewStackService, TabViewModel>> _tabViewModels;

        public BottomMenuViewModel(IParameterViewStackService parameterViewStackService)
            : base(parameterViewStackService)
        {
            TabViewModels = new List<Func<IParameterViewStackService, TabViewModel>>
            {
                service => new TabViewModel("Search", "", service, () => CreateMenuItem<StoreSearchViewModel>(service)),
                service => new TabViewModel("Queue", "", service, () => CreateMenuItem<QueuesViewModel>(service)),
                service => new TabViewModel("Me", "", service, () => CreateMenuItem<QueuesViewModel>(service))
            };
        }

        private static ViewModelBase CreateMenuItem<TViewModel>(IParameterViewStackService service)
            where TViewModel : ViewModelBase
        {
            var viewmodel = Locator.Current.GetService<TViewModel>();
            viewmodel.SetNavigationService(service);
            return viewmodel;
        }

        public List<Func<IParameterViewStackService, TabViewModel>> TabViewModels
        {
            get => _tabViewModels;
            set => this.RaiseAndSetIfChanged(ref _tabViewModels, value);
        }
    }
}