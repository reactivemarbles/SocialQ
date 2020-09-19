using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using SocialQ.Stores;
using Xamarin.Forms;

namespace SocialQ.Forms.Stores
{
    public partial class StoreSearch
    {
        public StoreSearch()
        {
            InitializeComponent();

            this.OneWayBind(ViewModel, x => x.IsLoading, x => x.Loading.IsRunning)
                .DisposeWith(PageDisposables);

            this.BindCommand(ViewModel, x => x.Search, x => x.Search, x => x.SearchText, nameof(Search.Clicked) )
                .DisposeWith(PageDisposables);

            this.WhenAnyValue(x => x.ViewModel)
                .Where(x => x != null)
                .Select(x => Unit.Default)
                .InvokeCommand(this, x => x.ViewModel.InitializeData)
                .DisposeWith(PageDisposables);

            this.WhenAnyValue(x => x.ViewModel.Stores)
                .Where(x => x != null)
                .BindTo(this, x => x.StoreList.ItemsSource)
                .DisposeWith(PageDisposables);

            this.WhenAnyValue(x => x.ViewModel.StoreCategories)
                .Where(x => x != null)
                .BindTo(this, x => x.Categories.ItemsSource)
                .DisposeWith(PageDisposables);

            SearchBar
                .Events()
                .TextChanged
                .Throttle(TimeSpan.FromMilliseconds(250), RxApp.TaskpoolScheduler)
                .Where(x => x?.OldTextValue?.Length > 0 && x.NewTextValue?.Length == 0)
                .Select(x => x.NewTextValue)
                .ObserveOn(RxApp.MainThreadScheduler)
                .InvokeCommand(this, x => x.ViewModel.Search)
                .DisposeWith(PageDisposables);

            StoreList
                .Events()
                .ItemTapped
                .Select(x => x.Item)
                .Cast<StoreCardViewModel>()
                .InvokeCommand(this, x => x.ViewModel.Details)
                .DisposeWith(PageDisposables);

            StoreList
                .Events()
                .ItemSelected
                .Subscribe(item => StoreList.SelectedItem = null)
                .DisposeWith(PageDisposables);
        }
    }
}