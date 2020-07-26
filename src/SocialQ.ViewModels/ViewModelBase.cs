using System;
using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI;
using Sextant;

namespace SocialQ.ViewModels
{
    public abstract class ViewModelBase : ReactiveObject, INavigable
    {
        public string Id { get; }

        protected virtual IObservable<Unit> WhenNavigatedTo(INavigationParameter parameter) => Observable.Return(Unit.Default);

        protected virtual IObservable<Unit> WhenNavigatedFrom(INavigationParameter parameter) => Observable.Return(Unit.Default);

        protected virtual IObservable<Unit> WhenNavigatingTo(INavigationParameter parameter) => Observable.Return(Unit.Default);

        IObservable<Unit> INavigated.WhenNavigatedTo(INavigationParameter parameter) => WhenNavigatedTo(parameter);

        IObservable<Unit> INavigated.WhenNavigatedFrom(INavigationParameter parameter) => WhenNavigatedFrom(parameter);

        IObservable<Unit> INavigating. WhenNavigatingTo(INavigationParameter parameter) => WhenNavigatingTo(parameter);
    }
}