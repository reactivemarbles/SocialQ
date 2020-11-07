using System.Reactive;
using System.Reactive.Linq;
using ReactiveMarbles.PropertyChanged;
using ReactiveUI;

namespace SocialQ.Forms.Profile
{
    public partial class User
    {
        public User()
        {
            InitializeComponent();

            var userNameChanged =
                this.WhenPropertyChanges(x => x.ViewModel.UserName)
                    .Select(x => x.value)
                    .Where(string.IsNullOrEmpty)
                    .Select(_ => Unit.Default);

            WhenAppearing
                .CombineLatest(userNameChanged, (appearing, username) => Unit.Default)
                .InvokeCommand(this, x => x.ViewModel.SignUp);
        }
    }
}