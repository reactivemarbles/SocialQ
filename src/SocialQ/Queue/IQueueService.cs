using System;
using DynamicData;
using SocialQ.Stores;

namespace SocialQ.Queue
{
    /// <summary>
    /// Interface representing a <see cref="IService"/> for <see cref="QueuedStoreDto"/>.
    /// </summary>
    public interface IQueueService : IService
    {
        /// <summary>
        /// Gets an <see cref="IObservableCache{TObject,TKey}"/> of <see cref="QueuedStoreDto"/>.
        /// </summary>
        IObservableCache<QueuedStoreDto, Guid> Queue { get; }

        /// <summary>
        /// Get the users queued stores.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="forceUpdate">Force an update.</param>
        /// <returns>An observable sequence of queued stores.</returns>
        IObservable<QueuedStoreDto> GetQueue(Guid userId, bool forceUpdate = true);

        /// <summary>
        /// Enqueues a store for the specified user.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="store">The store.</param>
        /// <returns>A queued store.</returns>
        IObservable<QueuedStoreDto> EnQueue(Guid userId, StoreDto store);
    }
}