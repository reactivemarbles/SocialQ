using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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