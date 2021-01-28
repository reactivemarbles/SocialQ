using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using DynamicData;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Sextant;
using Sextant.Plugins.Popup;
using Shiny.Notifications;

namespace SocialQ.Stores
{
    /// <summary>
    /// <see cref="ViewModelBase"/> to search stores.
    /// </summary>
    public class StoreSearchViewModel : ViewModelBase
    {
        private readonly BehaviorSubject<Func<StoreDto, bool>> _filterFunction = new BehaviorSubject<Func<StoreDto, bool>>(_ => false);
        private readonly IPopupViewStackService _popupViewStackService;
        private readonly IStoreService _storeService;
        private readonly INotificationManager _notificationManager;
        private readonly ObservableAsPropertyHelper<bool> _isLoading;
        private readonly ReadOnlyObservableCollection<StoreCardViewModel> _stores;
        private readonly ReadOnlyObservableCollection<string?> _storeNames;

        /// <summary>
        /// Initializes a new instance of the <see cref="StoreSearchViewModel"/> class.
        /// </summary>
        /// <param name="popupViewStackService">The popup view stack service.</param>
        /// <param name="storeService">The store service.</param>
        /// <param name="notificationManager">The notification manager.</param>
        public StoreSearchViewModel(
            IPopupViewStackService popupViewStackService,
            IStoreService storeService,
            INotificationManager notificationManager)
            : base(popupViewStackService)
        {
            _popupViewStackService = popupViewStackService;
            _storeService = storeService;
            _notificationManager = notificationManager;

            _filterFunction.DisposeWith(Subscriptions);

            _storeService
               .Stores
               .Connect()
               .RefCount()
               .Filter(_filterFunction.AsObservable())
               .Transform(x => new StoreCardViewModel(x))
               .ObserveOn(RxApp.MainThreadScheduler)
               .Bind(out _stores)
               .DisposeMany()
               .Subscribe()
               .DisposeWith(Subscriptions);

            _storeService
               .Metadata
               .ObserveOn(RxApp.MainThreadScheduler)
               .Bind(out _storeNames)
               .DisposeMany()
               .Subscribe()
               .DisposeWith(Subscriptions);

            var isLoading =
                this.WhenAnyObservable(
                    x => x.Search.IsExecuting,
                    x => x.InitializeData.IsExecuting,
                    x => x.Details.IsExecuting,
                    x => x.Category.IsExecuting,
                    (search, initialize, details, category) =>
                        search || initialize || details || category);

            isLoading
               .ToProperty(this, nameof(IsLoading), out _isLoading, deferSubscription: true)
               .DisposeWith(Subscriptions);

            var canExecute = isLoading.Select(x => !x).StartWith(true);

            Search = ReactiveCommand.CreateFromObservable(ExecuteSearch, canExecute);
            Details = ReactiveCommand.CreateFromObservable<StoreCardViewModel, Unit>(ExecuteDetails);
            InitializeData = ReactiveCommand.CreateFromObservable(ExecuteInitializeData);
            Category = ReactiveCommand.CreateFromObservable<string, Unit>(ExecuteCategory, canExecute);
        }

        /// <summary>
        /// Gets the category command.
        /// </summary>
        public ReactiveCommand<string, Unit> Category { get; }

        /// <summary>
        /// Gets the initialize command.
        /// </summary>
        public ReactiveCommand<Unit, Unit> InitializeData { get; }

        /// <summary>
        /// Gets the search command.
        /// </summary>
        public ReactiveCommand<Unit, Unit> Search { get; }

        /// <summary>
        /// Gets the details command.
        /// </summary>
        public ReactiveCommand<StoreCardViewModel, Unit> Details { get; }

        /// <summary>
        /// Gets or sets the search text.
        /// </summary>
        [Reactive] public string? SearchText { get; set; }

        /// <summary>
        /// Gets a value indicating whether it is loading.
        /// </summary>
        public bool IsLoading => _isLoading.Value;

        /// <summary>
        /// Gets a the stores.
        /// </summary>
        public ReadOnlyObservableCollection<StoreCardViewModel> Stores => _stores;

        /// <summary>
        /// Gets a the store names.
        /// </summary>
        public ReadOnlyObservableCollection<string?> StoreNames => _storeNames;

        /// <summary>
        /// Gets store categories.
        /// </summary>
        public IEnumerable<string> StoreCategories => StoreCategoryExtensions.ListEnumeration<StoreCategory>().Select(x => x.ToString());

        private IObservable<Unit> ExecuteInitializeData() => Observable
           .Create<Unit>(
                observer =>
                {
                    var disposable = new CompositeDisposable();

                    _storeService
                       .GetStoreMetadata()
                       .Select(_ => Unit.Default)
                       .Subscribe()
                       .DisposeWith(disposable);

                    _storeService
                       .GetStores()
                       .Select(_ => Unit.Default)
                       .Subscribe(observer)
                       .DisposeWith(disposable);

                    return disposable;
                });

        private IObservable<Unit> ExecuteDetails(StoreCardViewModel arg) => _popupViewStackService
           .PushPopup<StoreDetailViewModel>(new NavigationParameter { { WellKnownNavigationParameters.Id, arg.Id } });

        private IObservable<Unit> ExecuteSearch() => Observable
           .Create<Unit>(
                observer =>
                {
                    static Func<StoreDto, bool> Search(string? term) => dto => string.IsNullOrEmpty(term) || (dto?.Name?.ToLower().Contains(term?.ToLower()) ?? false);

                    _filterFunction.OnNext(Search(SearchText));

                    return _storeService
                       .GetStores(false)
                       .Select(_ => Unit.Default)
                       .Subscribe(observer);
                });

        private IObservable<Unit> ExecuteCategory(string category) => Observable
           .Create<Unit>(
                observer =>
                {
                    static Func<StoreDto, bool> Filter(string term) => dto => !string.IsNullOrEmpty(term) && dto.Category.ToString().ToLower().Contains(term.ToLower());

                    _filterFunction.OnNext(Filter(category));

                    return _storeService
                       .GetStoreMetadata(false)
                       .Select(_ => Unit.Default)
                       .Subscribe(observer);
                });
    }
}