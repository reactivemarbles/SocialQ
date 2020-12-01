using System;
using System.Reactive;

namespace SocialQ.Startup
{
    /// <summary>
    /// Interface representing a startup operation.
    /// </summary>
    public interface IStartupOperation
    {
        /// <summary>
        /// Starts the operation.
        /// </summary>
        /// <returns>A completion notification.</returns>
        IObservable<Unit> Start();

        /// <summary>
        /// Gets a value indicating whether this instance will execute the start method.
        /// </summary>
        /// <returns>Whether the instance will start.</returns>
        bool CanStart();
    }
}