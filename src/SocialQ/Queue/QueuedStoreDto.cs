using System;
using SocialQ.Stores;

namespace SocialQ.Queue
{
    /// <summary>
    /// Represents a queued store.
    /// </summary>
    public class QueuedStoreDto : DtoBase
    {
        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        public UserDto? User { get; set; }

        /// <summary>
        /// Gets or sets the store.
        /// </summary>
        public StoreDto? Store { get; set; }

        /// <summary>
        /// Gets or sets the remaining queue time for the user and store.
        /// </summary>
        public DateTimeOffset RemainingQueueTime { get; set; } = DateTimeOffset.UtcNow;
    }
}