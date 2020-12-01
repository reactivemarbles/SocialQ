using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveMarbles.PropertyChanged;
using ReactiveUI;

namespace SocialQ.Forms.Stores
{
    /// <summary>
    /// Represents store detail.
    /// </summary>
    public partial class StoreDetail
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StoreDetail"/> class.
        /// </summary>
        public StoreDetail()
        {
            InitializeComponent();

            this.WhenPropertyChanges(x => x.ViewModel.StoreId)
                .Select(x => x.value)
                .Where(x => x != Guid.Empty)
                .InvokeCommand(this, x => x.ViewModel.InitializeData)
                .DisposeWith(PageDisposables);
        }
    }
}