using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;

namespace SocialQ.Mocks
{
    public abstract class HubClientMock<T> : IHubClient<T>
    {
        private readonly Random _random = new Random();

        protected readonly List<T> Items = new List<T>();

        public IObservable<T> Connect(string channel) =>
            Observable
                .Interval(TimeSpan.FromSeconds(8))
                .Where(x => Items.Count > 0)
                .Select(x => Items[_random.Next(0, Items.Count)]);

        public IObservable<T> Invoke(string methodName) => Observable.Empty<T>();
    }
}