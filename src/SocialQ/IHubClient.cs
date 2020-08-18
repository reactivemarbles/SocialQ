using System;
using System.Reactive;
using System.Threading.Tasks;

namespace SocialQ
{
    public interface IHubClient<out T> : IDisposable
    {
        /// <summary>
        /// An observable emitting values from the hub.
        /// </summary>
        IObservable<T> Hub { get; }

        /// <summary>
        /// Connect to the Hub.
        /// </summary>
        /// <returns>A task to monitor the progress.</returns>
        IObservable<Unit> Connect();

        /// <summary>
        /// Connect to the Hub.
        /// </summary>
        /// <returns>A task to monitor the progress.</returns>
        IObservable<Unit> Connect(string channel);

        /// <summary>
        /// Invokes a method with the provided name.
        /// </summary>
        /// <param name="methodName">The method name.</param>
        /// <typeparam name="T">The return type.</typeparam>
        /// <returns>A completion value.</returns>
        IObservable<T> InvokeAsync(string methodName);
    }
}