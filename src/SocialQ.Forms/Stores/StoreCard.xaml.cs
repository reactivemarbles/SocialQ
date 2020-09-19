using System.Reactive.Disposables;
using ReactiveUI;

namespace SocialQ.Forms
{
    public partial class StoreCard
    {
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