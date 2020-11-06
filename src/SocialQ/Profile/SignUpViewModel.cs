using System;
using System.Reactive;
using ReactiveUI;
using Sextant;
using Sextant.Plugins.Popup;

namespace SocialQ.Profile
{
    public class SignUpViewModel : ViewModelBase
    {
        public SignUpViewModel(IPopupViewStackService popupViewStackService)
            : base(popupViewStackService)
        {
            Cancel = ReactiveCommand.CreateFromObservable(ExecuteCancel);
        }

        public ReactiveCommand<Unit, Unit> Cancel { get; set; }

        private IObservable<Unit> ExecuteCancel() => ViewStackService.PopModal();
    }
}