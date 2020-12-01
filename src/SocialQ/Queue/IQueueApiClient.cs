using System;
using Akavache;

namespace SocialQ.Queue
{
    /// <summary>
    /// Interface representing a <see cref="QueuedStoreDto"/> api client.
    /// </summary>
    public interface IQueueApiClient
    {
        /// <summary>
        /// Gets the <see cref="IBlobCache"/>.
        /// </summary>
        IBlobCache BlobCache { get; }

        /// <summary>
        /// Queues a user to a store.
        /// </summary>
        /// <param name="request">The enqueue request.</param>
        /// <returns>The updated queued store.</returns>
        IObservable<QueuedStoreDto> Enqueue(EnqueueRequest request);

        /// <summary>
        /// Gets queued items for a user.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="forceUpdate">Force an update.</param>
        /// <returns>The updated queued store.</returns>
        IObservable<QueuedStoreDto> GetQueue(Guid userId, bool forceUpdate = false);
    }
}