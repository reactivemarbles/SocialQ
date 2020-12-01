using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace SocialQ
{
    /// <summary>
    /// Base object to connect to signal r.
    /// </summary>
    /// <typeparam name="T">The stream type.</typeparam>
    public class SignalRHubClientBase<T> : IHubClient<T>
        where T : class
    {
        private readonly HubConnection _connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="SignalRHubClientBase{T}"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public SignalRHubClientBase(HubConnection connection) => _connection = connection;

        /// <inheritdoc/>
        public virtual IObservable<T> Connect(string channel) =>
            Observable.Create<T>(async (observer, cancellation) =>
            {
                var reader = await _connection.StreamAsChannelAsync<T>(channel, cancellation).ConfigureAwait(false);
                while (!cancellation.IsCancellationRequested && await reader.WaitToReadAsync(cancellation).ConfigureAwait(false))
                {
                    while (reader.TryRead(out var item))
                    {
                        observer.OnNext(item);
                    }
                }

                await reader.Completion.ConfigureAwait(false);
                return Disposable.Empty;
            });

        /// <inheritdoc/>
        public virtual IObservable<T> Invoke(string methodName) =>
            _connection
                .InvokeAsync<T>(methodName)
                .ToObservable();
    }
}