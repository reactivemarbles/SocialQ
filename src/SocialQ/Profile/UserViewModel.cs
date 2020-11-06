using System;
using System.Reactive;
using System.Reactive.Disposables;
using ReactiveUI;
using Sextant;
using Sextant.Plugins.Popup;

namespace SocialQ.Profile
{
    public class UserViewModel : ViewModelBase
    {
        private readonly ObservableAsPropertyHelper<string> _userName;

        public UserViewModel(IPopupViewStackService popupViewStackService, ISettings settings)
            : base(popupViewStackService)
        {
            settings
                .WhenAnyValue(x => x.UserName)
                .ToProperty(this, nameof(UserName), out _userName)
                .DisposeWith(Subscriptions);

            SignUp = ReactiveCommand.CreateFromObservable(ExecuteSignUp);
        }

        public ReactiveCommand<Unit, Unit> SignUp { get; set; }

        public string UserName => _userName.Value;

        private IObservable<Unit> ExecuteSignUp() => 
            ViewStackService.PushModal<SignUpViewModel>(withNavigationPage: true);
    }
}