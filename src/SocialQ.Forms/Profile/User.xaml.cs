using System.Reactive;
using System.Reactive.Linq;
using ReactiveMarbles.PropertyChanged;
using ReactiveUI;

namespace SocialQ.Forms.Profile
{
    /// <summary>
    /// Represents a user profile page.
    /// </summary>
    public partial class User
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        public User()
        {
            InitializeComponent();

            var userNameChanged =
                this.WhenPropertyValueChanges(x => x.ViewModel!.UserName)
                    .Where(string.IsNullOrEmpty)
                    .Select(_ => Unit.Default);

            WhenAppearing
                .CombineLatest(userNameChanged, (_, __) => Unit.Default)
                .InvokeCommand(this, x => x.ViewModel!.SignUp);
        }
    }
}