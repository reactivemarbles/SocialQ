using System;
using System.Collections.ObjectModel;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using Shiny;
using SocialQ.Queue;

namespace SocialQ.ViewModels.Queue
{
    public class QueuesViewModel : ViewModelBase
    {
        private readonly IQueueService _queueService;
        private readonly ReadOnlyObservableCollection<QueuedItemViewModel> _queue;

        public QueuesViewModel(IQueueService queueService)
        {
            _queueService = queueService;

            _queueService
                .Queue
                .Connect()
                .RefCount()
                .Transform(x => new QueuedItemViewModel(x))
                .AutoRefresh(x => x.CurrentQueueTime)
                .Sort(SortExpressionComparer<QueuedItemViewModel>.Descending(x => x.CurrentQueueTime))
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out _queue)
                .Subscribe()
                .DisposeWith(Subscriptions);

            Observable
                .Interval(TimeSpan.FromSeconds(5))
                .Subscribe(_ => _queueService.UpdateQueue())
                .DisposeWith(Subscriptions);
        }

        public ReadOnlyObservableCollection<QueuedItemViewModel> Queue => _queue;
    }
}