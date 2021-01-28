using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveMarbles.PropertyChanged;
using ReactiveUI;
using Sextant.Plugins.Popup;

namespace SocialQ.Startup
{
    /// <summary>
    /// ViewModel for the splash page.
    /// </summary>
    public class SplashViewModel : ViewModelBase
    {
        private readonly IAppStartup _appStartup;
        private readonly ObservableAsPropertyHelper<bool> _loading;

        /// <summary>
        /// Initializes a new instance of the <see cref="SplashViewModel"/> class.
        /// </summary>
        /// <param name="popupViewStackService">The popup view stack service.</param>
        /// <param name="appStartup">The app startup.</param>
        public SplashViewModel(IPopupViewStackService popupViewStackService, IAppStartup appStartup)
            : base(popupViewStackService)
        {
            _appStartup = appStartup;
            Initialize = ReactiveCommand.CreateFromObservable(ExecuteInitialize);
            Navigate = ReactiveCommand.CreateFromObservable(ExecuteNavigate);

            var initializing =
                this.WhenAnyObservable(x => x.Initialize.IsExecuting)
                   .StartWith(false);

            initializing
               .Zip(initializing.Skip(1), (first, second) => first && !second)
               .Where(x => x)
               .Select(_ => Unit.Default)
               .InvokeCommand(Navigate);

            _appStartup
               .WhenPropertyValueChanges(x => x.IsCompleted)
               .DistinctUntilChanged()
               .ToProperty(this, nameof(IsLoading), out _loading, scheduler: RxApp.MainThreadScheduler)
               .DisposeWith(Subscriptions);
        }

        /// <summary>
        /// Gets a <see cref="ReactiveCommand"/> that initializes this view model.
        /// </summary>
        public ReactiveCommand<Unit, Unit> Initialize { get; }

        /// <summary>
        /// Gets a <see cref="ReactiveCommand"/> that navigates from this view model.
        /// </summary>
        public ReactiveCommand<Unit, Unit> Navigate { get; }

        /// <summary>
        /// Gets a value indicating whether this view model is loading.
        /// </summary>
        public bool IsLoading => _loading.Value;

        /// <inheritdoc/>
        protected override IObservable<Unit> ExecuteInitialize() =>
            Observable.Create<Unit>(observer => _appStartup.Startup().Subscribe(observer));

        private IObservable<Unit> ExecuteNavigate() => ViewStackService.PushPage(new BottomMenuViewModel(ViewStackService), resetStack: true);
    }
}