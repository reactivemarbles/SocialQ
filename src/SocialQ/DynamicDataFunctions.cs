using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using DynamicData;

namespace SocialQ
{
    public static class DynamicDataFunctions
    {
        public static IObservable<T> AddOrUpdate<T>(this IObservable<T> source, SourceCache<T, Guid> sourceCache)
            where T : DtoBase => source.Do(sourceCache.AddOrUpdate);

        public static IObservable<IEnumerable<T>> AddOrUpdate<T>(this IObservable<IEnumerable<T>> source, SourceCache<T, Guid> sourceCache)
            where T : DtoBase => source.Do(sourceCache.AddOrUpdate);
    }
}