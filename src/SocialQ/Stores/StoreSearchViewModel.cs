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
using Rocket.Surgery.Xamarin.Essentials.Abstractions;
using Sextant;
using Sextant.Plugins.Popup;
using Shiny.Notifications;
using Xamarin.Essentials;

namespace SocialQ.Stores
{
    public class StoreSearchViewModel : ViewModelBase
    {
        private readonly BehaviorSubject<Func<StoreDto, bool>> _filterFunction = new BehaviorSubject<Func<StoreDto, bool>>(dto => false);
        private readonly IPopupViewStackService _popupViewStackService;
        private readonly IStoreService _storeService;
        private readonly INotificationManager _notificationManager;
        private readonly IDialogs _dialogs;
        private readonly IConnectivity _connectivity;
        private readonly ObservableAsPropertyHelper<bool> _isLoading;
        private readonly ReadOnlyObservableCollection<StoreCardViewModel> _stores;
        private readonly ReadOnlyObservableCollection<string> _storeNames;
        private string _searchText;
        private IObservable<ConnectivityChangedEventArgs> _iHasNoInternets;

        public StoreSearchViewModel(
            IPopupViewStackService popupViewStackService,
            IStoreService storeService,
            INotificationManager notificationManager,
            IDialogs dialogs,
            IConnectivity connectivity)
            : base(popupViewStackService)
        {
            _popupViewStackService = popupViewStackService;
            _storeService = storeService;
            _notificationManager = notificationManager;
            _dialogs = dialogs;
            _connectivity = connectivity;

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
                this.WhenAnyObservable(x => x.Search.IsExecuting,
                    x => x.InitializeData.IsExecuting,
                    x => x.Details.IsExecuting,
                    x => x.Category.IsExecuting,
                    (search, initialize, details, category) =>
                        search || initialize || details || category);

            isLoading
                .ToProperty(this, nameof(IsLoading), out _isLoading, deferSubscription: true)
                .DisposeWith(Subscriptions);

            _iHasNoInternets =  _connectivity.ConnectivityChanged.WhereAccessReasonsAreNot(NetworkAccess.Internet);

            _iHasNoInternets
                .Subscribe(_ => _dialogs.Snackbar($"The Internets Has Moved: {_.NetworkAccess}").Subscribe());

            _iHasNoInternets
                .Select(_ => _dialogs.Snackbar($"The Internets Has Moved: {_.NetworkAccess}"))
                .Switch()
                .Subscribe()
                .DisposeWith(Subscriptions);

            var canExecute = isLoading.Select(x => !x).StartWith(true);

            Search = ReactiveCommand.CreateFromObservable(ExecuteSearch, canExecute);
            Details = ReactiveCommand.CreateFromObservable<StoreCardViewModel, Unit>(ExecuteDetails);
            InitializeData = ReactiveCommand.CreateFromObservable(ExecuteInitializeData);
            Category = ReactiveCommand.CreateFromObservable<string, Unit>(ExecuteCategory, canExecute);
        }

        public ReactiveCommand<string,Unit> Category { get; set; }

        public ReactiveCommand<Unit, Unit> InitializeData { get; }

        public ReactiveCommand<Unit, Unit> Search { get; }

        public ReactiveCommand<StoreCardViewModel, Unit> Details { get; set; }

        public string SearchText
        {
            get => _searchText;
            set => this.RaiseAndSetIfChanged(ref _searchText, value);
        }

        public bool IsLoading => _isLoading.Value;

        public ReadOnlyObservableCollection<StoreCardViewModel> Stores => _stores;

        public ReadOnlyObservableCollection<string> StoreNames => _storeNames;

        public IEnumerable<string> StoreCategories => StoreCategoryExtensions.ListEnumeration<StoreCategory>().Select(x=>x.ToString());

        private IObservable<Unit> ExecuteInitializeData() =>
            Observable
                .Create<Unit>(observer =>
                {
                    var disposable = new CompositeDisposable();

                    _storeService
                        .GetStoreMetadata()
                        .Select(x => Unit.Default)
                        .TakeUntil(_iHasNoInternets)
                        .Subscribe()
                        .DisposeWith(disposable);

                    _storeService
                        .GetStores()
                        .Select(x => Unit.Default)
                        .TakeUntil(_iHasNoInternets)
                        .Subscribe(observer)
                        .DisposeWith(disposable);

                    return disposable;
                });

        private IObservable<Unit> ExecuteDetails(StoreCardViewModel arg) =>
            _popupViewStackService
                .PushPopup<StoreDetailViewModel>(new NavigationParameter {{WellKnownNavigationParameters.Id, arg.Id}});

        private IObservable<Unit> ExecuteSearch() =>
            Observable
                .Create<Unit>(observer =>
                {
                    static Func<StoreDto, bool> Search(string term) =>
                        dto => string.IsNullOrEmpty(term) || dto.Name.ToLower().Contains(term.ToLower());

                    _filterFunction.OnNext(Search(SearchText));

                    return _storeService
                        .GetStores(false)
                        .Select(x => Unit.Default)
                        .TakeUntil(_iHasNoInternets)
                        .Subscribe(observer);
                });

        private IObservable<Unit> ExecuteCategory(string category) => 
            Observable
                .Create<Unit>(observer =>
                {
                    static Func<StoreDto, bool> Filter(string term) =>
                        dto => !string.IsNullOrEmpty(term) && dto.Category.ToString().ToLower().Contains(term.ToLower());

                    _filterFunction.OnNext(Filter(category));

                    return _storeService
                        .GetStoreMetadata(false)
                        .Select(x => Unit.Default)
                        .Subscribe(observer);
                });
    }

    internal static class ConnectivityFunctions
    {
        public static IObservable<ConnectivityChangedEventArgs> WhereAccessReasonsAre(this IObservable<ConnectivityChangedEventArgs> connectionEvents,
            params NetworkAccess[] networkAccesses) =>
            connectionEvents.Where(x => networkAccesses.Contains(x.NetworkAccess));
        public static IObservable<ConnectivityChangedEventArgs> WhereAccessReasonsAreNot(this IObservable<ConnectivityChangedEventArgs> connectionEvents,
            params NetworkAccess[] networkAccesses) =>
            connectionEvents.Where(x => !networkAccesses.Contains(x.NetworkAccess));
    }
}