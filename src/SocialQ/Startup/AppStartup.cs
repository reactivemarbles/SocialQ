using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;

namespace SocialQ.Startup
{
    /// <summary>
    /// Represents the application startup sequence.
    /// </summary>
    public class AppStartup : IAppStartup
    {
        private readonly IEnumerable<IStartupOperation> _startupTasks;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppStartup"/> class.
        /// </summary>
        /// <param name="startupTasks">The startup tasks.</param>
        public AppStartup(IEnumerable<IStartupOperation> startupTasks) => _startupTasks = startupTasks;

        /// <inheritdoc/>
        public IObservable<Unit> Startup() =>
            Observable.Create<Unit>(observer =>
            {
                var disposable = new CompositeDisposable();

                foreach (var task in _startupTasks.Where(x => x.CanStart()))
                {
                    task.Start().ObserveOn(RxApp.MainThreadScheduler).Subscribe().DisposeWith(disposable);
                }

                observer.OnNext(Unit.Default);
                observer.OnCompleted();
                return disposable;
            });
    }
}