using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using Sextant;

namespace SocialQ.ViewModels
{
    public abstract class ViewModelBase : ReactiveObject, INavigable, IDestructible
    {
        public string Id { get; }

        protected CompositeDisposable Subscriptions { get; } = new CompositeDisposable();

        protected virtual IObservable<Unit> WhenNavigatedTo(INavigationParameter parameter) => Observable.Return(Unit.Default);

        protected virtual IObservable<Unit> WhenNavigatedFrom(INavigationParameter parameter) => Observable.Return(Unit.Default);

        protected virtual IObservable<Unit> WhenNavigatingTo(INavigationParameter parameter) => Observable.Return(Unit.Default);

        protected virtual void Destroy()
        {
        }

        IObservable<Unit> INavigated.WhenNavigatedTo(INavigationParameter parameter) => WhenNavigatedTo(parameter);

        IObservable<Unit> INavigated.WhenNavigatedFrom(INavigationParameter parameter) => WhenNavigatedFrom(parameter);

        IObservable<Unit> INavigating. WhenNavigatingTo(INavigationParameter parameter) => WhenNavigatingTo(parameter);

        void IDestructible.Destroy()
        {
            Destroy();
        }
    }
}