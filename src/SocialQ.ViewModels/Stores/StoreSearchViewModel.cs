using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;

namespace SocialQ.ViewModels.Stores
{
    public class StoreSearchViewModel : ViewModelBase
    {
        private readonly BehaviorSubject<Func<StoreDto, bool>> _searchFunction = new BehaviorSubject<Func<StoreDto, bool>>(dto => false);
        private readonly IStoreService _storeService;
        private readonly ObservableAsPropertyHelper<bool> _isLoading;
        private readonly ReadOnlyObservableCollection<StoreCardViewModel> _stores;
        private string _searchText;

        public StoreSearchViewModel(IStoreService storeService)
        {
            _storeService = storeService;

            this.WhenAnyObservable(
                    x => x.Search.IsExecuting,
                    x => x.InitializeData.IsExecuting,
                    (search, initialize) => search || initialize)
                .Skip(1)
                .DistinctUntilChanged()
                .ToProperty(this, nameof(IsLoading), out _isLoading, deferSubscription: true)
                .DisposeWith(Subscriptions);

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

            Search = ReactiveCommand.CreateFromObservable(ExecuteSearch);
            InitializeData = ReactiveCommand.CreateFromObservable(ExecuteInitializeData);
        }

        public string SearchText
        {
            get => _searchText;
            set => this.RaiseAndSetIfChanged(ref _searchText, value);
        }

        public bool IsLoading => _isLoading.Value;

        public ReactiveCommand<Unit, Unit> InitializeData { get; }

        public ReactiveCommand<Unit, Unit> Search { get; }

        public ReadOnlyObservableCollection<StoreCardViewModel> Stores => _stores;

        private IObservable<Unit> ExecuteInitializeData() => _storeService.GetStores().Select(x => Unit.Default);

        private IObservable<Unit> ExecuteSearch() =>
            Observable
                .Create<Unit>(observer =>
                {
                    var disposables = new CompositeDisposable();

                    Func<StoreDto, bool> Search(string term) =>
                        dto =>
                        {
                            if (string.IsNullOrEmpty(term))
                            {
                                return false;
                            }

                            return dto.Name.ToLower().Contains(term.ToLower());
                        };

                    _searchFunction.OnNext(Search(SearchText));

                    _storeService
                        .GetStores()
                        .Subscribe()
                        .DisposeWith(disposables);

                    return disposables;
                })
                .Delay(TimeSpan.FromSeconds(3), RxApp.TaskpoolScheduler)
                .ObserveOn(RxApp.MainThreadScheduler);
    }
}