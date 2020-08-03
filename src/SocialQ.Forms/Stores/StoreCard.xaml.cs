using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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