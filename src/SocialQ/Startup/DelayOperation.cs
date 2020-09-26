using System;
using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI;

namespace SocialQ.Startup
{
    public class DelayOperation : IStartupOperation
    {
        public IObservable<Unit> Start() =>
            Observable.Create<Unit>(observer =>
                Observable.Return(Unit.Default).Delay(TimeSpans.DefaultDelay, RxApp.MainThreadScheduler)
                    .ObserveOn(RxApp.MainThreadScheduler)
                    .Subscribe(observer));

        public bool CanStart()
        {
#if DEBUG
            return true;
#else
            return false;
#endif
        }
    }
}