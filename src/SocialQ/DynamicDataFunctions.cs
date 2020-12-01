using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using DynamicData;

namespace SocialQ
{
    /// <summary>
    /// Functions related to Dynamic Data.
    /// </summary>
    public static class DynamicDataFunctions
    {
        /// <summary>
        /// Add or updates values of a <see cref="SourceCache{TObject,TKey}"/>.
        /// </summary>
        /// <param name="source">The source observable.</param>
        /// <param name="sourceCache">The source cache.</param>
        /// <typeparam name="T">The type parameters.</typeparam>
        /// <returns>Returns the source observable.</returns>
        public static IObservable<T> AddOrUpdate<T>(this IObservable<T> source, SourceCache<T, Guid> sourceCache)
            where T : DtoBase => source.Do(sourceCache.AddOrUpdate);

        /// <summary>
        /// Add or updates values of a <see cref="SourceCache{TObject,TKey}"/>.
        /// </summary>
        /// <param name="source">The source observable.</param>
        /// <param name="sourceCache">The source cache.</param>
        /// <typeparam name="T">The type parameters.</typeparam>
        /// <returns>Returns the source observable.</returns>
        public static IObservable<IEnumerable<T>> AddOrUpdate<T>(this IObservable<IEnumerable<T>> source, SourceCache<T, Guid> sourceCache)
            where T : DtoBase => source.Do(sourceCache.AddOrUpdate);

        /// <summary>
        /// Add or updates values of a <see cref="SourceCache{TObject,TKey}"/>.
        /// </summary>
        /// <param name="source">The source observable.</param>
        /// <param name="sourceList">The source list.</param>
        /// <typeparam name="T">The type parameters.</typeparam>
        /// <returns>Returns the source observable.</returns>
        public static IObservable<IEnumerable<T>> AddRange<T>(this IObservable<IEnumerable<T>> source, SourceList<T> sourceList) => source.Do(sourceList.AddRange);
    }
}