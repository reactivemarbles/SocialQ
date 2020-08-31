using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using DynamicData;
using ReactiveUI;
using Sextant;

namespace SocialQ
{
    public class StoreSearchViewModel : ViewModelBase
    {
        private readonly BehaviorSubject<Func<StoreDto, bool>> _searchFunction = new BehaviorSubject<Func<StoreDto, bool>>(dto => false);
        private readonly IStoreService _storeService;
        private readonly ObservableAsPropertyHelper<bool> _isLoading;
        private readonly ReadOnlyObservableCollection<StoreCardViewModel> _stores;
        private ReadOnlyObservableCollection<string> _storeNames;
        private string _searchText;

        public StoreSearchViewModel(IParameterViewStackService parameterViewStackService, IStoreService storeService)
            : base(parameterViewStackService)
        {
            _storeService = storeService;

            _searchFunction.DisposeWith(Subscriptions);

            _storeService
                .Stores
                .Connect()
                .RefCount()
                .Filter(_searchFunction)
                .Transform(x => new StoreCardViewModel(x))
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out _stores)
                .DisposeMany()
                .Subscribe()
                .DisposeWith(Subscriptions);

            _storeService
                .Metadata
                .Bind(out _storeNames)
                .DisposeMany()
                .Subscribe()
                .DisposeWith(Subscriptions);

            var isLoading =
                this.WhenAnyObservable(
                        x => x.Search.IsExecuting,
                        x => x.InitializeData.IsExecuting,
                        (search, initialize) => search || initialize);

            isLoading
                .ToProperty(this, nameof(IsLoading), out _isLoading, deferSubscription: true, scheduler: RxApp.MainThreadScheduler)
                .DisposeWith(Subscriptions);

            Search = ReactiveCommand.CreateFromObservable<string, Unit>(ExecuteSearch);
            Details = ReactiveCommand.CreateFromObservable<StoreCardViewModel, Unit>(ExecuteDetails);
            InitializeData = ReactiveCommand.CreateFromObservable(ExecuteInitializeData);
        }

        public string SearchText
        {
            get => _searchText;
            set => this.RaiseAndSetIfChanged(ref _searchText, value);
        }

        public bool IsLoading => _isLoading.Value;

        public ReactiveCommand<Unit, Unit> InitializeData { get; }

        public ReactiveCommand<string, Unit> Search { get; }

        public ReactiveCommandBase<StoreCardViewModel, Unit> Details { get; set; }

        public ReadOnlyObservableCollection<StoreCardViewModel> Stores => _stores;

        private IObservable<Unit> ExecuteInitializeData() =>
            _storeService
                .GetStores()
                .Select(x => Unit.Default);


        private IObservable<Unit> ExecuteDetails(StoreCardViewModel arg) =>
            ViewStackService
                .PushModal<StoreDetailViewModel>(new NavigationParameter {{WellKnownNavigationParameters.Id, arg.Id}});

        private IObservable<Unit> ExecuteSearch(string searchText) =>
            Observable
                .Create<Unit>(observer =>
                {
                    Func<StoreDto, bool> Search(string term) =>
                        dto =>
                        {
                            if (string.IsNullOrEmpty(term))
                            {
                                return false;
                            }

                            return dto.Name.ToLower().Contains(term.ToLower());
                        };

                    _searchFunction.OnNext(Search(searchText));

                    return _storeService
                        .GetStores(false)
                        .Select(x => Unit.Default)
                        .Subscribe(observer);
                });
    }
}