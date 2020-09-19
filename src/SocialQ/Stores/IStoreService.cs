using System;
using System.Collections.Generic;
using DynamicData;

namespace SocialQ.Stores
{
    public interface IStoreService
    {
        IObservableCache<StoreDto, Guid> Stores { get; }

        IObservable<IChangeSet<string>> Metadata { get; }

        IObservable<StoreDto> GetStore(Guid id, bool forceUpdate = true);

        IObservable<IEnumerable<StoreDto>> GetStores(bool forceUpdate = true);

        IObservable<IEnumerable<string>> GetStoreMetadata(bool forceUpdate = true);
    }
}