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
    /// <summary>
    /// ViewModel for queues.
    /// </summary>
    public class QueuesViewModel : ViewModelBase
    {
        private readonly IQueueService _queueService;
        private readonly ReadOnlyObservableCollection<QueuedItemViewModel> _queue;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueuesViewModel"/> class.
        /// </summary>
        /// <param name="popupViewStackService">The popup view stack service.</param>
        /// <param name="queueService">The queue service.</param>
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

            InitializeData = ReactiveCommand.CreateFromObservable(ExecuteInitialize);
        }

        /// <summary>
        /// Gets a <see cref="ReactiveCommand"/> that initialized the data.
        /// </summary>
        public ReactiveCommand<Unit, Unit> InitializeData { get; }

        /// <summary>
        /// Gets the queues that are bound to the screen.
        /// </summary>
        public ReadOnlyObservableCollection<QueuedItemViewModel> Queue => _queue;

        /// <inheritdoc/>
        protected override IObservable<Unit> ExecuteInitialize() =>
            _queueService
                .GetQueue(Guid.Empty)
                .Select(_ => Unit.Default);
    }
}