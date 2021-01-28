using System;
using System.ComponentModel;
using System.Reactive;

namespace SocialQ.Startup
{
    /// <summary>
    /// Interface representing that application startup sequence.
    /// </summary>
    public interface IAppStartup : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets a value indicating whether the startup is complete.
        /// </summary>
        bool IsCompleted { get; }

        /// <summary>
        /// Starts the application life cycle.
        /// </summary>
        /// <returns>A completion notification.</returns>
        IObservable<Unit> Startup();
    }
}