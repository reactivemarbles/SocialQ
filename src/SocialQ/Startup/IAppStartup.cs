using System;
using System.Reactive;

namespace SocialQ.Startup
{
    /// <summary>
    /// Interface representing that application startup sequence.
    /// </summary>
    public interface IAppStartup
    {
        /// <summary>
        /// Starts the application life cycle.
        /// </summary>
        /// <returns>A completion notification.</returns>
        IObservable<Unit> Startup();
    }
}