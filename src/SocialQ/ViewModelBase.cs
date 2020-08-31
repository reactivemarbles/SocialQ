using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using ReactiveUI;
using Sextant;

namespace SocialQ
{
    public abstract class ViewModelBase : ReactiveObject, INavigable, IDestructible
    {
        protected ViewModelBase(IParameterViewStackService parameterViewStackService)
        {
            ViewStackService = parameterViewStackService;
        }

        public string Id { get; }

        protected CompositeDisposable Subscriptions { get; } = new CompositeDisposable();

        protected IParameterViewStackService ViewStackService { get; }

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