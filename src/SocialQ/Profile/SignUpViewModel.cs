using System;
using System.Reactive;
using ReactiveUI;
using Sextant;

namespace SocialQ.Profile
{
    public class SignUpViewModel : ViewModelBase
    {
        public SignUpViewModel(IParameterViewStackService parameterViewStackService)
            : base(parameterViewStackService)
        {
            Cancel = ReactiveCommand.CreateFromObservable(ExecuteCancel);
        }

        public ReactiveCommand<Unit, Unit> Cancel { get; set; }

        private IObservable<Unit> ExecuteCancel() => ViewStackService.PopModal();
    }
}