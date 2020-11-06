using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace SocialQ
{
    public class SignalRHubClientBase<T> : IHubClient<T>
        where T : class
    {
        private readonly HubConnection _connection;

        public SignalRHubClientBase(HubConnection connection)
        {
            _connection = connection;
        }

        public virtual IObservable<T> Connect(string channel) =>
            Observable.Create<T>(async (observer, cancellation) =>
            {
                var reader = await _connection.StreamAsChannelAsync<T>(channel, cancellation);
                while (!cancellation.IsCancellationRequested && await reader.WaitToReadAsync(cancellation))
                {
                    while (reader.TryRead(out var item))
                    {
                        observer.OnNext(item);
                    }
                }

                await reader.Completion;
                return Disposable.Empty;
            });

        public virtual IObservable<T> Invoke(string methodName) =>
            _connection
                .InvokeAsync<T>(methodName)
                .ToObservable();
    }
}