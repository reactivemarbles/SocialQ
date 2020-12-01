using System;
using System.Reactive;
using ReactiveUI;
using Sextant;
using Sextant.Plugins.Popup;

namespace SocialQ.Profile
{
    /// <summary>
    /// <see cref="ViewModelBase"/> for user sign up.
    /// </summary>
    public class SignUpViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SignUpViewModel"/> class.
        /// </summary>
        /// <param name="popupViewStackService">The popup view stack service.</param>
        public SignUpViewModel(IPopupViewStackService popupViewStackService)
            : base(popupViewStackService) => Cancel = ReactiveCommand.CreateFromObservable(ExecuteCancel);

        /// <summary>
        /// Gets the cancel command.
        /// </summary>
        public ReactiveCommand<Unit, Unit> Cancel { get; }

        private IObservable<Unit> ExecuteCancel() => ViewStackService.PopModal();
    }
}