using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using DynamicData;

namespace SocialQ
{
    public static class StoreServiceFunctions
    {
        public static IObservable<StoreDto> AddOrUpdate(this IObservable<StoreDto> source,
            SourceCache<StoreDto, Guid> stores) => source.Do(stores.AddOrUpdate);

        public static IObservable<IEnumerable<StoreDto>> AddOrUpdate(this IObservable<IEnumerable<StoreDto>> source,
            SourceCache<StoreDto, Guid> stores) => source.Do(stores.AddOrUpdate);
    }
}