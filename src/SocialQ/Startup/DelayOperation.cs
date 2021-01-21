using System;
using System.Reactive;
using System.Reactive.Linq;

namespace SocialQ.Startup
{
    /// <summary>
    /// Represents a startup delay for debugging.
    /// </summary>
    public class DelayOperation : StartupOperationBase
    {
        /// <inheritdoc/>
        protected override IObservable<Unit> Start() => Observable.Return(Unit.Default).Delay(TimeSpans.DefaultDelay);

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