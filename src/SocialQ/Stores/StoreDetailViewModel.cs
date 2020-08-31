using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Sextant;

namespace SocialQ
{
    public class StoreDetailViewModel : ViewModelBase
    {
        private readonly IStoreService _storeService;

        public StoreDetailViewModel(IParameterViewStackService parameterViewStackService, IStoreService storeService)
            : base(parameterViewStackService)
        {
            _storeService = storeService;

            InitializeData = ReactiveCommand.CreateFromObservable<Guid, Unit>(ExecuteInitializeData);

            this.WhenAnyValue(x => x.StoreId)
                .InvokeCommand(this, x => x.InitializeData)
                .DisposeWith(Subscriptions);
        }

        [Reactive] public Guid StoreId { get; set; }

        [ObservableAsProperty] public StoreDto Store { get; }

        public ReactiveCommand<Guid, Unit> InitializeData { get; }

        protected override IObservable<Unit> WhenNavigatingTo(INavigationParameter parameter)
        {
            parameter.TryGetValue(WellKnownNavigationParameters.Id, out object id);
            StoreId = (Guid) id;
            return Observable.Return(Unit.Default);
        }

        private IObservable<Unit> ExecuteInitializeData(Guid id) =>
            Observable
                .Create<Unit>(observer => 
                    _storeService
                        .GetStore(id)
                        .ToPropertyEx(this, x => x.Store, deferSubscription: true));
    }
}