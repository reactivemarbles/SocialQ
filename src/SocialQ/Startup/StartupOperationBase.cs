using System;
using System.Reactive;
using System.Reactive.Linq;

namespace SocialQ.Startup
{
    /// <summary>
    /// Represents a base startup operation.
    /// </summary>
    public class StartupOperationBase : IStartupOperation
    {
        /// <inheritdoc/>
        IObservable<Unit> IStartupOperation.Start() => Start();

        /// <inheritdoc/>
        bool IStartupOperation.CanStart() => CanStart();

        /// <summary>
        /// Template method for the startup operation.
        /// </summary>
        /// <returns>A completion notification.</returns>
        protected virtual IObservable<Unit> Start() => Observable.Empty(Unit.Default);

        /// <summary>
        /// Template method for whether or not the startup operation will execute.
        /// </summary>
        /// <returns>A completion notification.</returns>
        protected virtual bool CanStart() => true;
    }
}