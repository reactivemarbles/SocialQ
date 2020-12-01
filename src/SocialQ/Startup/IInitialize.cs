using System;
using System.Reactive;

namespace SocialQ.Startup
{
    /// <summary>
    /// Interface representing an entity that can initialize.
    /// </summary>
    public interface IInitialize
    {
        /// <summary>
        /// Initialize the instance.
        /// </summary>
        /// <returns>A completion notification.</returns>
        IObservable<Unit> Initialize();
    }
}