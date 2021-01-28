using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace SocialQ.Startup
{
    /// <summary>
    /// Represents the application startup sequence.
    /// </summary>
    public class AppStartup : ReactiveObject, IAppStartup
    {
        private readonly IEnumerable<IStartupOperation> _startupTasks;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppStartup"/> class.
        /// </summary>
        /// <param name="startupTasks">The startup tasks.</param>
        public AppStartup(IEnumerable<IStartupOperation> startupTasks) => _startupTasks = startupTasks;

        /// <inheritdoc/>
        [Reactive] public bool IsCompleted { get; set; }

        /// <inheritdoc/>
        public IObservable<Unit> Startup() => _startupTasks
           .Where(x => x.CanStart())
           .Select(x => x.Start())
           .Concat()
           .Finally(() => IsCompleted = true);
    }
}