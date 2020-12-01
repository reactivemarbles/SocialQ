using System;
using System.Collections.Generic;
using DynamicData;

namespace SocialQ.Stores
{
    /// <summary>
    /// Interface representing a service to provide store data.
    /// </summary>
    public interface IStoreService
    {
        /// <summary>
        /// Gets an <see cref="IObservableCache{TObject,TKey}"/> of <see cref="StoreDto"/>.
        /// </summary>
        IObservableCache<StoreDto, Guid> Stores { get; }

        /// <summary>
        /// Gets an observable of store metadata.
        /// </summary>
        IObservable<IChangeSet<string?>> Metadata { get; }

        /// <summary>
        /// Gets a <see cref="StoreDto"/> with the specified id.
        /// </summary>
        /// <param name="id">The store id.</param>
        /// <param name="forceUpdate">Force an update.</param>
        /// <returns>A store.</returns>
        IObservable<StoreDto> GetStore(Guid id, bool forceUpdate = true);

        /// <summary>
        /// Gets an enumerable of stores.
        /// </summary>
        /// <param name="forceUpdate">Force an update.</param>
        /// <returns>A list of stores.</returns>
        IObservable<IEnumerable<StoreDto>> GetStores(bool forceUpdate = true);

        /// <summary>
        /// Gets an enumerable of store names.
        /// </summary>
        /// <param name="forceUpdate">Force an update.</param>
        /// <returns>A list of store names.</returns>
        IObservable<IEnumerable<string?>> GetStoreMetadata(bool forceUpdate = true);
    }
}