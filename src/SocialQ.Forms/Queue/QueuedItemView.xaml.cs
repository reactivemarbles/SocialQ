using System.Reactive.Disposables;
using ReactiveUI;
using SocialQ.ViewModels.Queue;

namespace SocialQ.Forms.Queue
{
    public partial class QueuedItemView : ContentViewBase<QueuedItemViewModel>
    {
        public QueuedItemView()
        {
            InitializeComponent();

            this.OneWayBind(ViewModel, x => x.Name, x => x.StoreName.Text)
                .DisposeWith(ViewDisposables);

            this.OneWayBind(ViewModel, x => x.RemainingQueueTime, x => x.RemainingQueue.Text)
                .DisposeWith(ViewDisposables);

            this.OneWayBind(ViewModel, x => x.CurrentQueueTime, x => x.CurrentTime.Text)
                .DisposeWith(ViewDisposables);
        }
    }
}