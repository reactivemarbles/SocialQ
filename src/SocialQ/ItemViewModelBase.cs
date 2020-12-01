using System;
using System.Reactive.Disposables;
using ReactiveUI;

namespace SocialQ
{
    /// <summary>
    /// Base abstraction for a ViewModel.
    /// </summary>
    public abstract class ItemViewModelBase : ReactiveObject, IDisposable
    {
        /// <summary>
        /// Gets the subscription disposable.
        /// </summary>
        protected CompositeDisposable Subscriptions { get; } = new CompositeDisposable();

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

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