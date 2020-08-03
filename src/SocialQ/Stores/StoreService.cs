using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using DynamicData;

namespace SocialQ
{
    public class StoreService : IStoreService
    {
        private readonly SourceCache<StoreDto, Guid> _stores = new SourceCache<StoreDto, Guid>(x => x.Id);
        private readonly IStoreApiClient _apiClient;

        public StoreService(IStoreApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public IObservable<IEnumerable<StoreDto>> GetStores(bool forceUpdate = true) =>
            _apiClient
                .GetStores(forceUpdate)
                .EditDiff(_stores);

        public IObservable<StoreDto> GetStore(Guid id, bool forceUpdate = true) =>
            _apiClient
                .GetStore(id, forceUpdate)
                .Do(store => _stores.AddOrUpdate(store));

        public IObservableCache<StoreDto, Guid> Stores => _stores;
    }
}