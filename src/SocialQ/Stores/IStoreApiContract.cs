using System;
using System.Collections.Generic;
using Refit;

namespace SocialQ.Stores
{
    public interface IStoreApiContract
    {
        /// <summary>
        /// Gets an store with the specified ID.
        /// </summary>
        /// <param name="storeId">The ID of the appointment to retrieve.</param>
        /// <param name="parameters">The azure function parameters.</param>
        /// <returns>An observable which signals with the store.</returns>
        [Get("/api/stores/{storeId}")]
        IObservable<StoreDto> GetStore(Guid storeId, [Query] FunctionParameters parameters);

        /// <summary>
        /// Gets all the available stores.
        /// </summary>
        /// <param name="parameters">The azure function parameters.</param>
        /// <returns>An observable which signals with the stores.</returns>
        [Get("/api/stores")]
        IObservable<IEnumerable<StoreDto>> GetStores([Query] FunctionParameters parameters);

        /// <summary>
        /// Gets all the available stores.
        /// </summary>
        /// <param name="parameters">The azure function parameters.</param>
        /// <returns>An observable which signals with the stores.</returns>
        [Get("/api/metadata/stores")]
        IObservable<IEnumerable<string>> GetMetadata([Query] FunctionParameters parameters);
    }
}