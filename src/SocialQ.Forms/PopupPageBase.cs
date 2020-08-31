using System.Reactive.Disposables;
using Sextant;
using Sextant.Plugins.Popup;

namespace SocialQ.Forms
{
    public abstract class PopupPageBase<TViewModel> : SextantPopupPage<TViewModel>
        where TViewModel : ViewModelBase
    {
        protected CompositeDisposable PageDisposables { get; } = new CompositeDisposable();
    }
}