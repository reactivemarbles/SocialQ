using System;
using System.Collections.Generic;
using Akavache;
using Splat;

namespace SocialQ
{
    public class StoreApiClient : IStoreApiClient
    {
        private readonly IStoreApiContract _apiContract;
        private readonly IBlobCache _blobCache;
        private readonly IFullLogger _logger;

        public StoreApiClient(IStoreApiContract apiContract, IBlobCache blobCache, IFullLogger logger)
        {
            _apiContract = apiContract;
            _blobCache = blobCache;
            _logger = logger;
        }

        public IObservable<StoreDto> GetStore(Guid storeId, bool forceUpdate = false) =>
            _apiContract
                .GetStore(storeId)
                .CacheApiResult($"{nameof(GetStore)}-{storeId}", _blobCache, _logger, forceUpdate: forceUpdate);

        public IObservable<IEnumerable<StoreDto>> GetStores(bool forceUpdate = false) =>
            _apiContract
                .GetStores()
                .CacheApiResult($"{nameof(GetStores)}", _blobCache, _logger, forceUpdate: forceUpdate);
    }
}