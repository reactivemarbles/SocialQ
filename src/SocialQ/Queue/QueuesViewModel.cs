using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using Sextant;
using Sextant.Plugins.Popup;

namespace SocialQ.Queue
{
    public class QueuesViewModel : ViewModelBase
    {
        private readonly IQueueService _queueService;
        private readonly ReadOnlyObservableCollection<QueuedItemViewModel> _queue;

        public QueuesViewModel(IPopupViewStackService popupViewStackService, IQueueService queueService)
            : base(popupViewStackService)
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
                .DisposeMany()
                .Subscribe()
                .DisposeWith(Subscriptions);

            Observable
                .Interval(TimeSpan.FromSeconds(5))
                .Subscribe(_ => _queueService.GetQueue(Guid.Empty))
                .DisposeWith(Subscriptions);

            InitializeData = ReactiveCommand.CreateFromObservable(ExecuteInitializeData);
        }

        public ReactiveCommand<Unit, Unit> InitializeData { get; set; }

        public ReadOnlyObservableCollection<QueuedItemViewModel> Queue => _queue;

        private IObservable<Unit> ExecuteInitializeData() =>
            _queueService
                .GetQueue(Guid.Empty)
                .Select(_ => Unit.Default);
    }
}