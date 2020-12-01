using System.Reactive.Disposables;
using Sextant.Plugins.Popup;

namespace SocialQ.Forms
{
    /// <summary>
    /// Abstraction representing a <see cref="SextantPopupPage{TViewModel}"/>.
    /// </summary>
    /// <typeparam name="TViewModel">The view model type.</typeparam>
    public abstract class PopupPageBase<TViewModel> : SextantPopupPage<TViewModel>
        where TViewModel : ViewModelBase
    {
        /// <summary>
        /// Gets the page disposable.
        /// </summary>
        protected CompositeDisposable PageDisposables { get; } = new CompositeDisposable();
    }
}