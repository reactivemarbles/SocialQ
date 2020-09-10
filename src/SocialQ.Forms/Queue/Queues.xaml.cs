using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using SocialQ.Queue;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SocialQ.Forms.Queue
{
    public partial class Queues
    {
        public Queues()
        {
            InitializeComponent();

            this.WhenAnyValue(x => x.ViewModel.Queue)
                .Where(x => x != null)
                .BindTo(this, x => x.Queue.ItemsSource)
                .DisposeWith(PageDisposables);

            this.WhenAnyValue(x => x.ViewModel)
                .Where(x => x != null)
                .Select(x => Unit.Default)
                .InvokeCommand(this, x => x.ViewModel.InitializeData)
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