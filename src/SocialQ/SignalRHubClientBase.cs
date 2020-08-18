using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reactive.Threading.Tasks;
using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace SocialQ
{
    public class SignalRHubClientBase<T> : IHubClient<T>
        where T : class
    {
        private readonly CompositeDisposable _compositeDisposable = new CompositeDisposable();
        private readonly SerialDisposable _connectionDisposable = new SerialDisposable();
        private readonly ReplaySubject<T> _replaySubject = new ReplaySubject<T>(1);
        private readonly HubConnection _connection;

        public SignalRHubClientBase(HubConnection connection)
        {
            _connection = connection;
            _replaySubject.DisposeWith(_compositeDisposable);
        }

        public virtual IObservable<Unit> Connect() => _connection.StartAsync().ToObservable();

        public IObservable<Unit> Connect(string channel) =>
            Observable.Create<Unit>(observer =>
            {
                var disposable = new CompositeDisposable();
                    _connection
                        .StreamAsChannelAsync<T>(channel)
                        .ToObservable()
                        .Publish()
                        .Subscribe(channelReader =>
                            (channelReader.ReadAsync() as Task<T>)
                            .ToObservable()
                            .Subscribe(value =>
                                _replaySubject.OnNext(value))
                            .DisposeWith(disposable))
                        .DisposeWith(disposable);

                    _connectionDisposable.Disposable = disposable;
                    return _connectionDisposable.Disposable;
            });

        public virtual IObservable<T> InvokeAsync(string methodName) =>
            _connection
                .InvokeAsync<T>(methodName)
                .ToObservable();

        public IObservable<T> Hub => _replaySubject.AsObservable();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _connectionDisposable?.Dispose();
                _compositeDisposable?.Dispose();
            }
        }
    }
}