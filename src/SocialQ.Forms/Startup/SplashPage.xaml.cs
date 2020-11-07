using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveMarbles.PropertyChanged;
using ReactiveUI;

namespace SocialQ.Forms.Startup
{
    public partial class SplashPage
    {
        public SplashPage()
        {
            InitializeComponent();

            this.OneWayBind(ViewModel, x => x.IsLoading, x => x.Loading.IsRunning)
                .DisposeWith(PageDisposables);

            this.WhenPropertyChanges(x => x.ViewModel)
                .Where(x => x.value != null)
                .Select(x => Unit.Default)
                .InvokeCommand(this, x => x.ViewModel.Initialize);
        }
    }
}