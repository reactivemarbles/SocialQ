using System.Reactive.Disposables;
using ReactiveUI;

namespace SocialQ.Forms.Profile
{
    /// <summary>
    /// Represents the sign up page.
    /// </summary>
    public partial class SignUp
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SignUp"/> class.
        /// </summary>
        public SignUp()
        {
            InitializeComponent();

            this.BindCommand(ViewModel!, x => x!.Cancel, x => x.Cancel, nameof(Cancel.Pressed))
                .DisposeWith(PageDisposables);
        }
    }
}