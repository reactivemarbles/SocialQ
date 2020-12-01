using System;
using System.Collections.Generic;
using Akavache;
using Splat;

namespace SocialQ.Stores
{
    /// <summary>
    /// The api for the store client.
    /// </summary>
    public class StoreApiClient : IStoreApiClient
    {
        private readonly IStoreApiContract _apiContract;
        private readonly IBlobCache _blobCache;
        private readonly IFullLogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="StoreApiClient"/> class.
        /// </summary>
        /// <param name="apiContract">The api contract.</param>
        /// <param name="blobCache">The blob cache.</param>
        /// <param name="logger">The logger.</param>
        public StoreApiClient(IStoreApiContract apiContract, IBlobCache blobCache, ILogManager logger)
        {
            _apiContract = apiContract;
            _blobCache = blobCache;
            _logger = logger.GetLogger(GetType());
        }

        /// <inheritdoc/>
        public IObservable<StoreDto> GetStore(Guid storeId, bool forceUpdate = false) =>
            _apiContract
                .GetStore(storeId, FunctionParameters.Default)
                .CacheApiResult($"{nameof(GetStore)}-{storeId}", _blobCache, _logger, forceUpdate: forceUpdate);

        /// <inheritdoc/>
        public IObservable<IEnumerable<StoreDto>> GetStores(bool forceUpdate = false) =>
            _apiContract
                .GetStores(FunctionParameters.Default)
                .CacheApiResult($"{nameof(GetStores)}", _blobCache, _logger, forceUpdate: forceUpdate);

        /// <inheritdoc/>
        public IObservable<IEnumerable<string?>> GetStoreMetadata(bool forceUpdate = false) =>
            _apiContract
                .GetMetadata(FunctionParameters.Default)
                .CacheApiResult($"{nameof(GetStoreMetadata)}", _blobCache, _logger, forceUpdate: forceUpdate);
    }
}