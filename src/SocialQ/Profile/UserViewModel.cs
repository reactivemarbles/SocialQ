using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveMarbles.PropertyChanged;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Sextant;
using Sextant.Plugins.Popup;

namespace SocialQ.Profile
{
    /// <summary>
    /// <see cref="ViewModelBase"/> for a system user.
    /// </summary>
    public class UserViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserViewModel"/> class.
        /// </summary>
        /// <param name="popupViewStackService">The popup view stack service.</param>
        /// <param name="settings">The settings.</param>
        public UserViewModel(IPopupViewStackService popupViewStackService, ISettings settings)
            : base(popupViewStackService)
        {
            settings
                .WhenPropertyValueChanges(x => x.UserName)
               .ToProperty(this, nameof(UserName), string.Empty)
                .DisposeWith(Subscriptions);

            SignUp = ReactiveCommand.CreateFromObservable(ExecuteSignUp);
        }

        /// <summary>
        /// Gets the sign up command.
        /// </summary>
        public ReactiveCommand<Unit, Unit> SignUp { get; }

        /// <summary>
        /// Gets the user name.
        /// </summary>
        public string? UserName { get; }

        private IObservable<Unit> ExecuteSignUp() =>
            ViewStackService.PushModal<SignUpViewModel>(withNavigationPage: true);
    }
}