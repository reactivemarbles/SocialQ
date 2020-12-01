using System.Reactive.Disposables;
using ReactiveUI;

namespace SocialQ.Forms.Queue
{
    /// <summary>
    /// Represents a queued item.
    /// </summary>
    public partial class QueuedItemView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueuedItemView"/> class.
        /// </summary>
        public QueuedItemView()
        {
            InitializeComponent();

            this.OneWayBind(ViewModel, x => x.Name, x => x.StoreName.Text)
                .DisposeWith(ViewDisposables);

            this.OneWayBind(ViewModel, x => x.RemainingQueueTime, x => x.RemainingQueue.Text)
                .DisposeWith(ViewDisposables);

            this.OneWayBind(ViewModel, x => x.CurrentQueueTime, x => x.CurrentTime.Text, remainingTime => $"{remainingTime:hh\\:mm\\:ss}")
                .DisposeWith(ViewDisposables);
        }
    }
}