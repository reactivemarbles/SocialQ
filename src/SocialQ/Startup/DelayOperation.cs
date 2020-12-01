using System;
using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI;

namespace SocialQ.Startup
{
    /// <summary>
    /// Represents a startup delay for debugging.
    /// </summary>
    public class DelayOperation : StartupOperationBase
    {
        /// <inheritdoc/>
        protected override IObservable<Unit> Start() =>
            Observable.Create<Unit>(observer =>
                Observable.Return(Unit.Default).Delay(TimeSpans.DefaultDelay, RxApp.MainThreadScheduler)
                    .ObserveOn(RxApp.MainThreadScheduler)
                    .Subscribe(observer));

        /// <inheritdoc/>
        protected override bool CanStart()
        {
#if DEBUG
            return true;
#else
            return false;
#endif
        }
    }
}