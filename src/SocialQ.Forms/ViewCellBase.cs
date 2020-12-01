using System.Reactive.Disposables;
using ReactiveUI;
using ReactiveUI.XamForms;

namespace SocialQ.Forms
{
    /// <summary>
    /// Represents a <see cref="ReactiveViewCell{TViewModel}"/>.
    /// </summary>
    /// <typeparam name="TViewModel">The view model type.</typeparam>
    public class ViewCellBase<TViewModel> : ReactiveViewCell<TViewModel>
        where TViewModel : ReactiveObject
    {
        /// <summary>
        /// Gets the view cell disposable.
        /// </summary>
        protected CompositeDisposable ViewCellDisposables { get; } = new CompositeDisposable();
    }
}