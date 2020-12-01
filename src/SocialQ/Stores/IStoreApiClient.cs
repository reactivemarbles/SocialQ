using System;
using System.Collections.Generic;

namespace SocialQ.Stores
{
    /// <summary>
    /// Interface representing an api client for <see cref="StoreDto"/>.
    /// </summary>
    public interface IStoreApiClient
    {
        /// <summary>
        /// Get a store with the provided id.
        /// </summary>
        /// <param name="storeId">The store id.</param>
        /// <param name="forceUpdate">Force an update.</param>
        /// <returns>A store dto.</returns>
        IObservable<StoreDto> GetStore(Guid storeId, bool forceUpdate = false);

        /// <summary>
        /// Gets a list of stores.
        /// </summary>
        /// <param name="forceUpdate">Force an update.</param>
        /// <returns>A list of stores.</returns>
        IObservable<IEnumerable<StoreDto>> GetStores(bool forceUpdate = false);

        /// <summary>
        /// Gets a list of store names.
        /// </summary>
        /// <param name="forceUpdate">Force an update.</param>
        /// <returns>A list of stores.</returns>
        IObservable<IEnumerable<string?>> GetStoreMetadata(bool forceUpdate = false);
    }
}