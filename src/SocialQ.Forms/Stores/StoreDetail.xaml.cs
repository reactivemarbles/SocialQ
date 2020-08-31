using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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