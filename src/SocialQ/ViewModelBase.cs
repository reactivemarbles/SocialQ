using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using Sextant;
using Sextant.Plugins.Popup;
using SocialQ.Startup;

namespace SocialQ
{
    /// <summary>
    /// Base abstraction for a ViewModel.
    /// </summary>
    public abstract class ViewModelBase : ReactiveObject, IInitialize, INavigable, IDestructible, IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelBase"/> class.
        /// </summary>
        /// <param name="popupViewStackService">The popup view stack service.</param>
        protected ViewModelBase(IPopupViewStackService popupViewStackService) => ViewStackService = popupViewStackService;

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public string Id { get; protected set; } = string.Empty;

        /// <summary>
        /// Gets the subscription disposable.
        /// </summary>
        protected CompositeDisposable Subscriptions { get; } = new CompositeDisposable();

        /// <summary>
        /// Gets the view stack service.
        /// </summary>
        protected IPopupViewStackService ViewStackService { get; private set; }

        /// <summary>
        /// Sets the navigation service for the view model.
        /// </summary>
        /// <param name="viewStackService">The view stack service.</param>
        public void SetNavigationService(IPopupViewStackService viewStackService) => ViewStackService = viewStackService;

        /// <inheritdoc/>
        IObservable<Unit> INavigated.WhenNavigatedTo(INavigationParameter parameter) => WhenNavigatedTo(parameter);

        /// <inheritdoc/>
        IObservable<Unit> INavigated.WhenNavigatedFrom(INavigationParameter parameter) => WhenNavigatedFrom(parameter);

        /// <inheritdoc/>
        IObservable<Unit> INavigating.WhenNavigatingTo(INavigationParameter parameter) => WhenNavigatingTo(parameter);

        /// <inheritdoc/>
        IObservable<Unit> IInitialize.Initialize() => ExecuteInitialize();

        /// <inheritdoc/>
        void IDestructible.Destroy() => Destroy();

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// An observable sequence that notifies subscribers this item was navigated to.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>An observable sequence.</returns>
        protected virtual IObservable<Unit> WhenNavigatedTo(INavigationParameter parameter) => Observable.Empty<Unit>();

        /// <summary>
        /// An observable sequence that notifies subscribers this item was navigated from.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>An observable sequence.</returns>
        protected virtual IObservable<Unit> WhenNavigatedFrom(INavigationParameter parameter) => Observable.Empty<Unit>();

        /// <summary>
        /// An observable sequence that notifies subscribers this item was navigating to.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>An observable sequence.</returns>
        protected virtual IObservable<Unit> WhenNavigatingTo(INavigationParameter parameter) => Observable.Empty<Unit>();

        /// <summary>
        /// An observable sequence that notifies subscribers this item is initializing.
        /// </summary>
        /// <returns>An observable sequence.</returns>
        protected virtual IObservable<Unit> ExecuteInitialize() => Observable.Empty<Unit>();

        /// <summary>
        /// A method that executes when the view model is removed from the stack.
        /// </summary>
        protected virtual void Destroy() => Subscriptions.Dispose();

        /// <summary>
        /// Disposes of resources.
        /// </summary>
        /// <param name="disposing">A value indicating whether the instance is disposing.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Subscriptions.Dispose();
            }
        }
    }
}