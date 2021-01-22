using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveMarbles.PropertyChanged;
using ReactiveUI;
using Xamarin.Forms;

namespace SocialQ.Forms.Queue
{
    /// <summary>
    /// Represents a queues of stores..
    /// </summary>
    public partial class Queues
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Queues"/> class.
        /// </summary>
        public Queues()
        {
            InitializeComponent();

            this.WhenPropertyChanges(x => x.ViewModel!.Queue)
                .Where(x => x.Value != null)
                .Select(x => x.Value)
                .BindTo(this, x => x.Queue.ItemsSource)
                .DisposeWith(PageDisposables);

            this.WhenPropertyChanges(x => x.ViewModel)
                .Where(x => x.Value != null)
                .Select(x => Unit.Default)
                .InvokeCommand(this, x => x.ViewModel!.InitializeData)
                .DisposeWith(PageDisposables);

            Queue
                .Events()
                .ItemSelected
                .Subscribe(_ =>
                {
                    Queue.SelectedItem = null;
                })
                .DisposeWith(PageDisposables);
        }
    }
}