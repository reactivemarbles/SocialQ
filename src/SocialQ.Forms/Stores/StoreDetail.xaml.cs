using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;

namespace SocialQ.Forms.Stores
{
    public partial class StoreDetail
    {
        public StoreDetail()
        {
            InitializeComponent();

            this.WhenAnyValue(x => x.ViewModel.StoreId)
                .Where(x => x != Guid.Empty)
                .InvokeCommand(this, x => x.ViewModel.InitializeData)
                .DisposeWith(PageDisposables);
        }
    }
}