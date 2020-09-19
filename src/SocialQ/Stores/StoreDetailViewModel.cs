using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Sextant;
using Sextant.Plugins.Popup;
using SocialQ.Queue;
using SocialQ.Resources;

namespace SocialQ
{
    public class StoreDetailViewModel : ViewModelBase
    {
        private readonly IPopupViewStackService _popupViewStackService;
        private readonly IStoreService _storeService;
        private readonly IQueueService _queueService;
        private readonly IDialogs _dialogs;

        public StoreDetailViewModel(
            IParameterViewStackService parameterViewStackService,
            IPopupViewStackService popupViewStackService,
            IStoreService storeService,
            IQueueService queueService,
            IDialogs dialogs)
            : base(parameterViewStackService)
        {
            _popupViewStackService = popupViewStackService;
            _storeService = storeService;
            _queueService = queueService;
            _dialogs = dialogs;


            var getStore =
                ReactiveCommand.CreateFromObservable<Guid, Unit>(ExecuteGetStore);

            this.WhenAnyValue(x => x.StoreId)
                .Where(x => x != Guid.Empty)
                .DistinctUntilChanged()
                .InvokeCommand(getStore)
                .DisposeWith(Subscriptions);

            InitializeData = ReactiveCommand.CreateFromObservable<Guid, Unit>(ExecuteInitializeData);
            Add = ReactiveCommand.CreateFromObservable(ExecuteAdd);
        }

        [Reactive] public Guid StoreId { get; set; }

        [Reactive] public StoreDto Store { get; set;}

        public ReactiveCommand<Guid, Unit> InitializeData { get; }

        public ReactiveCommand<Unit, Unit> Add { get; }

        protected override IObservable<Unit> WhenNavigatingTo(INavigationParameter parameter) =>
            Observable
                .Create<Unit>(_ =>
                {
                    parameter.TryGetValue(WellKnownNavigationParameters.Id, out object id);
                    StoreId = (Guid) id;
                    return Disposable.Empty;
                });

        private IObservable<Unit> ExecuteInitializeData(Guid id) =>
            Observable
                .Create<Unit>(_ => Disposable.Empty);

        private IObservable<Unit> ExecuteAdd() =>
            _queueService
                .EnQueue(Guid.Empty, Store)
                .Select(x => _popupViewStackService.PopPopup())
                .Switch()
                .Select(x => _dialogs.Snackbar(string.Format(Strings.AddedToQueue, Store.Name)))
                .Switch();

        private IObservable<Unit> ExecuteGetStore(Guid arg) =>
            Observable
                .Create<Unit>(_ =>
                    _storeService
                        .GetStore(arg)
                        .Subscribe(x => Store = x));
    }
}