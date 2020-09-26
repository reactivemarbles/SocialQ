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
using Sextant;
using Sextant.Plugins.Popup;
using Shiny.Notifications;

namespace SocialQ.Stores
{
    public class StoreSearchViewModel : ViewModelBase
    {
        private readonly BehaviorSubject<Func<StoreDto, bool>> _filterFunction = new BehaviorSubject<Func<StoreDto, bool>>(dto => false);
        private readonly IPopupViewStackService _popupViewStackService;
        private readonly IStoreService _storeService;
        private readonly INotificationManager _notificationManager;
        private readonly ObservableAsPropertyHelper<bool> _isLoading;
        private readonly ReadOnlyObservableCollection<StoreCardViewModel> _stores;
        private readonly ReadOnlyObservableCollection<string> _storeNames;
        private string _searchText;

        public StoreSearchViewModel(
            IParameterViewStackService parameterViewStackService,
            IPopupViewStackService popupViewStackService,
            IStoreService storeService,
            INotificationManager notificationManager)
            : base(parameterViewStackService)
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
                this.WhenAnyObservable(x => x.Search.IsExecuting,
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
                        .Subscribe()
                        .DisposeWith(disposable);

                    _storeService
                        .GetStores()
                        .Select(x => Unit.Default)
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
                    Func<StoreDto, bool> Search(string term) =>
                        dto =>
                        {
                            if (string.IsNullOrEmpty(term))
                            {
                                return true;
                            }

                            return dto.Name.ToLower().Contains(term.ToLower());
                        };

                    _filterFunction.OnNext(Search(SearchText));

                    return _storeService
                        .GetStores(false)
                        .Select(x => Unit.Default)
                        .Subscribe(observer);
                });

        private IObservable<Unit> ExecuteCategory(string category) => 
            Observable
                .Create<Unit>(observer =>
                {
                    Func<StoreDto, bool> Filter(string term) =>
                        dto =>
                        {
                            if (string.IsNullOrEmpty(term))
                            {
                                return false;
                            }

                            return dto.Category.ToString().ToLower().Contains(term.ToLower());
                        };

                    _filterFunction.OnNext(Filter(category));

                    return _storeService
                        .GetStoreMetadata(false)
                        .Select(x => Unit.Default)
                        .Subscribe(observer);
                });
    }
}