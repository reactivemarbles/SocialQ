using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;

namespace SocialQ.Mocks
{
    /// <summary>
    /// Represents a mock signal r connection.
    /// </summary>
    /// <typeparam name="T">The object type.</typeparam>
    public abstract class HubClientMock<T> : IHubClient<T>
    {
        private readonly List<T> _items = new List<T>();

        private readonly Random _random = new Random();

        /// <inheritdoc/>
        public IObservable<T> Connect(string channel) =>
            Observable
                .Interval(TimeSpan.FromSeconds(8))
                .Where(x => _items.Count > 0)
                .Select(x => _items[_random.Next(0, _items.Count)]);

        /// <inheritdoc/>
        public IObservable<T> Invoke(string methodName) => Observable.Empty<T>();
    }
}