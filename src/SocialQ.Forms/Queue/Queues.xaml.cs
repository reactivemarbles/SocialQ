using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using SocialQ.ViewModels.Queue;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SocialQ.Forms.Queue
{
    public partial class Queues : ContentPageBase<QueuesViewModel>
    {
        public Queues()
        {
            InitializeComponent();

            this.WhenAnyValue(x => x.ViewModel.Queue)
                .Where(x => x != null)
                .BindTo(this, x => x.Queue.ItemsSource)
                .DisposeWith(PageDisposables);
        }
    }
}