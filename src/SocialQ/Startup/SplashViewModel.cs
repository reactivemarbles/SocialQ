using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using Sextant;
using Sextant.Plugins.Popup;

namespace SocialQ.Startup
{
    public class SplashViewModel : ViewModelBase
    {
        private readonly IAppStartup _appStartup;
        private readonly ObservableAsPropertyHelper<bool> _loading;

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
                .ToProperty(this, nameof(IsLoading), out _loading)
                .DisposeWith(Subscriptions);

            initializing
                .Zip(initializing.Skip(1), (first, second) => first && !second)
                .Where( x => x)
                .Select(x => Unit.Default)
                .InvokeCommand(Navigate);
        }

        public ReactiveCommand<Unit, Unit> Initialize { get; }

        public ReactiveCommand<Unit, Unit> Navigate { get; }

        public bool IsLoading => _loading.Value;

        private IObservable<Unit> ExecuteInitialize() =>
            Observable.Create<Unit>(observer => _appStartup.Startup().Subscribe(observer));

        private IObservable<Unit> ExecuteNavigate() => ViewStackService.PushPage(new BottomMenuViewModel(ViewStackService), resetStack: true);
    }
}