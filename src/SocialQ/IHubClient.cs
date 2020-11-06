using System;

namespace SocialQ
{
    public interface IHubClient<out T>
    {
        /// <summary>
        /// Connect to the Hub.
        /// </summary>
        /// <returns>A task to monitor the progress.</returns>
        IObservable<T> Connect(string channel);

        /// <summary>
        /// Invokes a method with the provided name.
        /// </summary>
        /// <param name="methodName">The method name.</param>
        /// <typeparam name="T">The return type.</typeparam>
        /// <returns>A completion value.</returns>
        IObservable<T> Invoke(string methodName);
    }
}