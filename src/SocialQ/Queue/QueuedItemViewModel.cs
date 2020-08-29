using System;
using System.Reactive.Linq;
using ReactiveUI;

namespace SocialQ.Queue
{
    public class QueuedItemViewModel : ReactiveObject
    {
        private string _name;
        private Guid _id;
        private DateTimeOffset _remainingQueueTime;
        private readonly ObservableAsPropertyHelper<TimeSpan> _currentQueueTime;

        public QueuedItemViewModel(QueuedStoreDto dto)
        {
            Id = dto.Id;
            Name = dto.Store.Name;
            RemainingQueueTime = dto.RemainingQueueTime;

            this.WhenAnyValue(x => x.RemainingQueueTime)
                .RemainingTime(RxApp.TaskpoolScheduler)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToProperty(this, nameof(CurrentQueueTime), out _currentQueueTime, dto.RemainingQueueTime.TimeOfDay - DateTimeOffset.Now.TimeOfDay);
        }

        public Guid Id
        {
            get => _id;
            set => this.RaiseAndSetIfChanged(ref _id, value);
        }

        public string Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }

        public DateTimeOffset RemainingQueueTime
        {
            get => _remainingQueueTime;
            set => this.RaiseAndSetIfChanged(ref _remainingQueueTime, value);
        }

        public TimeSpan CurrentQueueTime => _currentQueueTime.Value;
    }
}