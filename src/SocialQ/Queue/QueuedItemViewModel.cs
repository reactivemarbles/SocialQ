using System;
using System.Reactive.Linq;
using ReactiveMarbles.PropertyChanged;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace SocialQ.Queue
{
    /// <summary>
    /// <see cref="ItemViewModelBase"/> for a <see cref="QueuedStoreDto"/>.
    /// </summary>
    public class QueuedItemViewModel : ItemViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueuedItemViewModel"/> class.
        /// </summary>
        /// <param name="dto">The queued store.</param>
        public QueuedItemViewModel(QueuedStoreDto dto)
        {
            Id = dto.Id;
            Name = dto.Store?.Name ?? string.Empty;
            RemainingQueueTime = dto.RemainingQueueTime;

            this.WhenPropertyChanges(x => x.RemainingQueueTime)
                .Select(x => x.value)
                .RemainingTime(RxApp.TaskpoolScheduler)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToProperty(this, nameof(CurrentQueueTime), dto.RemainingQueueTime.TimeOfDay - DateTimeOffset.Now.TimeOfDay);
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [Reactive] public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Reactive] public string Name { get; set; }

        /// <summary>
        /// Gets or sets the remaining queue time.
        /// </summary>
        [Reactive] public DateTimeOffset RemainingQueueTime { get; set; }

        /// <summary>
        /// Gets the current queue time.
        /// </summary>
        public TimeSpan CurrentQueueTime { get; }
    }
}