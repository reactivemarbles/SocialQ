using System;
using System.Reactive;
using System.Reactive.Linq;

namespace SocialQ.Mocks
{
    public abstract class HubClientMock<T> : IHubClient<T>
    {
        public abstract IObservable<T> Hub { get; }

        public IObservable<Unit> Connect() => Observable.Return(Unit.Default);

        public IObservable<Unit> Connect(string channel) => Observable.Return(Unit.Default);

        public IObservable<T> InvokeAsync(string methodName) => Observable.Empty<T>();

        public void Dispose()
        {
        }
    }
}