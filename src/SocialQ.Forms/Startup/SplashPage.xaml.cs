using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveMarbles.PropertyChanged;
using ReactiveUI;
using Xamarin.Forms;

namespace SocialQ.Forms.Startup
{
    /// <summary>
    /// Represents the splash page.
    /// </summary>
    public partial class SplashPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SplashPage"/> class.
        /// </summary>
        public SplashPage()
        {
            InitializeComponent();

            this.OneWayBind(ViewModel, x => x.IsLoading, x => x.Loading.IsRunning)
                .DisposeWith(PageDisposables);

            this.WhenPropertyValueChanges(x => x.ViewModel)
               .CombineLatest(this.Events().Appearing, (viewModel, appearing) => (viewModel, appearing))
               .Select(x => Unit.Default)
               .InvokeCommand(this, x => x.ViewModel!.Initialize);
        }
    }
}