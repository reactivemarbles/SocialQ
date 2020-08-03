using System;
using System.Collections.Generic;
using Refit;

namespace SocialQ
{
    public interface IStoreApiContract
    {
        /// <summary>
        /// Gets an store with the specified ID.
        /// </summary>
        /// <param name="storeId">The ID of the appointment to retrieve.</param>
        /// <returns>An observable which signals with the store.</returns>
        [Get("/stores/{storeId}")]
        IObservable<StoreDto> GetStore(Guid storeId);

        /// <summary>
        /// Gets all the available stores.
        /// </summary>
        /// <returns>An observable which signals with the stores.</returns>
        [Get("/stores")]
        IObservable<IEnumerable<StoreDto>> GetStores();
    }
}