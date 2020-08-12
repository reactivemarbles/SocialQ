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

        public StoreApiClient(IStoreApiContract apiContract, IBlobCache blobCache, ILogManager logger)
        {
            _apiContract = apiContract;
            _blobCache = blobCache;
            _logger = logger.GetLogger(GetType());
        }

        public IObservable<StoreDto> GetStore(Guid storeId, bool forceUpdate = false) =>
            _apiContract
                .GetStore(storeId, FunctionParameters.Default)
                .CacheApiResult($"{nameof(GetStore)}-{storeId}", _blobCache, _logger, forceUpdate: forceUpdate);

        public IObservable<IEnumerable<StoreDto>> GetStores(bool forceUpdate = false) =>
            _apiContract
                .GetStores(FunctionParameters.Default)
                .CacheApiResult($"{nameof(GetStores)}", _blobCache, _logger, forceUpdate: forceUpdate);

        public IObservable<IEnumerable<string>> GetStoreMetadata(bool forceUpdate = false) =>
            _apiContract
                .GetMetadata(FunctionParameters.Default)
                .CacheApiResult($"{nameof(GetStoreMetadata)}", _blobCache, _logger, forceUpdate: forceUpdate);
    }
}