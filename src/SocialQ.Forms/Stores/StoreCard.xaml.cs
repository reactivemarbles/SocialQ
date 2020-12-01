using System.Reactive.Disposables;
using ReactiveUI;

namespace SocialQ.Forms
{
    /// <summary>
    /// Represents a store card.
    /// </summary>
    public partial class StoreCard
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StoreCard"/> class.
        /// </summary>
        public StoreCard()
        {
            InitializeComponent();

            this.OneWayBind(ViewModel, x => x.Name, x => x.StoreName.Text)
                .DisposeWith(ViewCellDisposables);

            this.OneWayBind(ViewModel, x => x.OpeningTime, x => x.StoreOpenTime.Text, time => $"Opening Time: {time:hh:mm}")
                .DisposeWith(ViewCellDisposables);

            this.OneWayBind(ViewModel, x => x.CloseTime, x => x.StoreCloseTime.Text, time => $"Closing Time: {time:hh:mm}")
                .DisposeWith(ViewCellDisposables);
        }
    }
}