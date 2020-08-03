using System;
using System.Collections.Generic;

namespace SocialQ
{
    public interface IStoreApiClient
    {
        IObservable<StoreDto> GetStore(Guid storeId, bool forceUpdate = false);

        IObservable<IEnumerable<StoreDto>> GetStores(bool forceUpdate = false);
    }
}