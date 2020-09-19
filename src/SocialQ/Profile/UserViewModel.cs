using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using Sextant;

namespace SocialQ.Profile
{
    public class UserViewModel : ViewModelBase
    {
        private readonly ObservableAsPropertyHelper<string> _userName;

        public UserViewModel(IParameterViewStackService parameterViewStackService, ISettings settings)
            : base(parameterViewStackService)
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
            ViewStackService.PushModal<SignUpViewModel>();
    }
}