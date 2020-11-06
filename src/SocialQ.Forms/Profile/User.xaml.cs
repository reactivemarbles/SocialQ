using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI;

namespace SocialQ.Forms.Profile
{
    public partial class User
    {
        public User()
        {
            InitializeComponent();

            var userNameChanged =
                this.WhenAnyValue(x => x.ViewModel.UserName)
                    .Where(string.IsNullOrEmpty)
                    .Select(_ => Unit.Default);

            WhenAppearing
                .CombineLatest(userNameChanged, (appearing, username) => Unit.Default)
                .InvokeCommand(this, x => x.ViewModel.SignUp);
        }
    }
}