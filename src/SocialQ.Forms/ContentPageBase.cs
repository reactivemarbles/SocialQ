using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using ReactiveUI.XamForms;

namespace SocialQ.Forms
{
    /// <summary>
    /// Represents a base <see cref="ReactiveContentPage{TViewModel}"/>.
    /// </summary>
    /// <typeparam name="TViewModel">The view model type.</typeparam>
    public class ContentPageBase<TViewModel> : ReactiveContentPage<TViewModel>, IDisposable
        where TViewModel : ViewModelBase
    {
        private readonly AsyncSubject<Unit> _appearing = new AsyncSubject<Unit>();

        /// <summary>
        /// Gets an observable sequence indicating when a page is appearing.
        /// </summary>
        protected IObservable<Unit> WhenAppearing => _appearing.AsObservable();

        /// <summary>
        /// Gets the page disposable.
        /// </summary>
        protected CompositeDisposable PageDisposables { get; } = new CompositeDisposable();

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <inheritdoc/>
        protected override void OnAppearing()
        {
            _appearing.OnNext(Unit.Default);
            _appearing.OnCompleted();
        }

        /// <summary>
        /// Disposes of the resources.
        /// </summary>
        /// <param name="disposing">A value indicating whether the instance is disposing.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _appearing.Dispose();
                PageDisposables.Dispose();
            }
        }
    }
}