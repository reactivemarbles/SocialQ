using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using SocialQ.Startup;

namespace SocialQ.Splash
{
    public class AppStartup : IAppStartup
    {
        private readonly IEnumerable<IStartupTask> _startupTasks;

        public AppStartup(IEnumerable<IStartupTask> startupTasks)
        {
            _startupTasks = startupTasks;
        }

        public IObservable<Unit> Startup() =>
            Observable.Create<Unit>(observer =>
            {
                var disposable = new CompositeDisposable();

                foreach (var task in _startupTasks.Where(x => x.CanStart()))
                {
                    task.Start().Subscribe().DisposeWith(disposable);
                }

                observer.OnNext(Unit.Default);
                observer.OnCompleted();
                return disposable;
            });
    }
}