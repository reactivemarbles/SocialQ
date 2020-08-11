using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using Akavache;
using DynamicData;
using Splat;

namespace SocialQ
{
    public static class Extensions
    {
        // <summary>
        /// Places the contents of a cached IChangeSet into a Akavache data store.
        /// This means it will only retrieve values if the cache has expired.
        /// </summary>
        /// <typeparam name="TSource">The type of the source value of the change set.</typeparam>
        /// <typeparam name="TKey">The type of the key of the change set.</typeparam>
        /// <param name="source">The original change set to cache.</param>
        /// <param name="cacheKey"></param>
        /// <param name="blobCache"></param>
        /// <param name="log">A logger to provide debug and error information to.</param>
        /// <returns>An observable which provides caching support.</returns>
        public static IObservable<IChangeSet<TSource, TKey>> CacheChangeSet<TSource, TKey>(
            this IObservable<IChangeSet<TSource, TKey>> source,
            string cacheKey,
            IBlobCache blobCache,
            IFullLogger? log = default)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (cacheKey == null)
            {
                throw new ArgumentNullException(nameof(cacheKey));
            }

            return Observable
                .Create<IChangeSet<TSource, TKey>>(observer =>
                    source
                        .ToCollection()
                        .Concat(
                            blobCache
                                .GetObject<List<TSource>>(cacheKey)
                                .Catch(Observable.Return(new List<TSource>())))
                        .Subscribe(items =>
                        {
                            log?.Debug("CACHE: Writing {Count} items to cache with key: {CacheKey}", items.Count, cacheKey);

                            blobCache
                                .InsertObject(cacheKey, items.ToList())
                                .Catch(Observable.Return(Unit.Default).Do(unit => log?.Error("Failed to add items to cache")));
                        }));
        }

        public static IObservable<T> CacheApiResult<T>(
            this IObservable<T> source,
            string cacheKey,
            IBlobCache blobCache,
            IFullLogger logger,
            IScheduler? scheduler = null,
            bool forceUpdate = false,
            TimeSpan expiration = default)
        {
            expiration = expiration == TimeSpan.Zero ? Constants.DefaultCacheExpirationTimeOut : expiration;

            if (forceUpdate)
            {
                // TODO: [rlittlesii: July 30, 2020] Add retry and cached
                return source.SelectMany(async value =>
                {
                    await blobCache.InsertObject(cacheKey, value, expiration);
                    return value;
                });
            }

            blobCache
                .GetObject<T>(cacheKey)
                .Subscribe(obj => logger.Debug("Found: {@Object}", obj));

            // TODO: [rlittlesii: July 30, 2020] Add retry and cached
            return blobCache
                .GetOrFetchObject(
                    cacheKey,
                    () => source.Timeout(Constants.DefaultRequestTimeout), DateTimeOffset.Now.Add(expiration));
        }
    }
}