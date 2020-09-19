using System.Reactive.Disposables;
using ReactiveUI;
using Xamarin.Forms.Markup;

namespace SocialQ.Forms.Profile
{
    public partial class SignUp
    {
        public SignUp()
        {
            InitializeComponent();

            this.BindCommand(ViewModel, x => x.Cancel, x => x.Cancel, nameof(Cancel.Pressed))
                .DisposeWith(PageDisposables);
        }
    }
}