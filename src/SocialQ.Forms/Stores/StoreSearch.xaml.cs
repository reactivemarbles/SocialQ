using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using Xamarin.Forms;

namespace SocialQ.Forms.Stores
{
    public partial class StoreSearch
    {
        public StoreSearch()
        {
            InitializeComponent();

            this.WhenAnyValue(x => x.ViewModel.InitializeData)
                .SelectMany(command => command.Execute())
                .Subscribe()
                .DisposeWith(PageDisposables);

            this.WhenAnyValue(x => x.ViewModel.Stores)
                .Where(x => x != null)
                .BindTo(this, x => x.StoreList.ItemsSource)
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

            Search
                .Events()
                .Pressed
                .Select(x => SearchBar.Text)
                .InvokeCommand(this, x => x.ViewModel.Search)
                .DisposeWith(PageDisposables);

            StoreList
                .Events()
                .ItemSelected
                .Subscribe(item =>
                {
                    StoreList.SelectedItem = null;
                })
                .DisposeWith(PageDisposables);
        }
    }
}