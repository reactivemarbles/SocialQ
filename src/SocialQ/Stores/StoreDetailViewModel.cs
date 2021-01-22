using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveMarbles.PropertyChanged;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Sextant;
using Sextant.Plugins.Popup;
using SocialQ.Queue;
using SocialQ.Resources;

namespace SocialQ.Stores
{
    /// <inheritdoc />
    public class StoreDetailViewModel : ViewModelBase
    {
        private readonly IPopupViewStackService _popupViewStackService;
        private readonly IStoreService _storeService;
        private readonly IQueueService _queueService;
        private readonly IDialogs _dialogs;
        private Guid _storeId;

        /// <summary>
        /// Initializes a new instance of the <see cref="StoreDetailViewModel"/> class.
        /// </summary>
        /// <param name="popupViewStackService">The popup view stack service.</param>
        /// <param name="storeService">The store service.</param>
        /// <param name="queueService">The queue service.</param>
        /// <param name="dialogs">The dialogs.</param>
        public StoreDetailViewModel(
            IPopupViewStackService popupViewStackService,
            IStoreService storeService,
            IQueueService queueService,
            IDialogs dialogs)
            : base(popupViewStackService)
        {
            _popupViewStackService = popupViewStackService;
            _storeService = storeService;
            _queueService = queueService;
            _dialogs = dialogs;

            var getStore =
                ReactiveCommand.CreateFromObservable<Guid, Unit>(ExecuteGetStore);

            this.WhenPropertyChanges(x => x.StoreId)
               .Select(x => x.Value)
               .Where(x => x != Guid.Empty)
               .DistinctUntilChanged()
               .InvokeCommand(getStore)
               .DisposeWith(Subscriptions);

            InitializeData = ReactiveCommand.CreateFromObservable<Guid, Unit>(ExecuteInitializeData);
            Add = ReactiveCommand.CreateFromObservable(ExecuteAdd);
        }

        /// <summary>
        /// Gets or sets the store id.
        /// </summary>
        public Guid StoreId { get => _storeId; set => this.RaiseAndSetIfChanged(ref _storeId, value); }

        /// <summary>
        /// Gets or sets the store.
        /// </summary>
        [Reactive] public StoreDto? Store { get; set; }

        /// <summary>
        /// Gets the initialize data command.
        /// </summary>
        public ReactiveCommand<Guid, Unit> InitializeData { get; }

        /// <summary>
        /// Gets the initialize data command.
        /// </summary>
        public ReactiveCommand<Unit, Unit> Add { get; }

        /// <inheritdoc/>
        protected override IObservable<Unit> WhenNavigatingTo(INavigationParameter parameter) => Observable
           .Create<Unit>(
                _ =>
                {
                    if (parameter.TryGetValue(WellKnownNavigationParameters.Id, out object id))
                    {
                        if (Guid.TryParse(id.ToString(), out var storeId))
                        {
                            StoreId = storeId;
                        }
                    }

                    return Disposable.Create(() => { });
                });

        private IObservable<Unit> ExecuteInitializeData(Guid id) =>
            Observable
                .Create<Unit>(_ => Disposable.Empty);

        private IObservable<Unit> ExecuteAdd() =>
            _queueService
                .EnQueue(Guid.Empty, Store!)
                .Select(_ => _popupViewStackService.PopPopup())
                .Switch()
                .Select(_ => _dialogs.Snackbar(string.Format(Strings.AddedToQueue!, Store?.Name)))
                .Switch();

        private IObservable<Unit> ExecuteGetStore(Guid arg) =>
            Observable
                .Create<Unit>(_ =>
                    _storeService
                        .GetStore(arg)
                        .Subscribe(x => Store = x));
    }
}