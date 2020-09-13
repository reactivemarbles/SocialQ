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
        }
    }
}