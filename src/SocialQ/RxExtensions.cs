using System;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using Splat;

namespace SocialQ
{

    /// <summary>
    /// Provides extension methods for the Reactive Extensions provide.
    /// </summary>
    public static class RxExtensions
    {
        /// <summary>
        /// Gets the default strategy for API retry. This provides the time based on the retry attempt number.
        /// </summary>
        public static Func<int, TimeSpan> DefaultStrategy =>
            n => TimeSpan.FromSeconds(Math.Min(Math.Pow(2.0, n), 180.0));

        /// <summary>
        /// Converts an observable into having exponential backoff retry support.
        /// </summary>
        /// <typeparam name="T">The type of the observable.</typeparam>
        /// <param name="source">The observable to add support to.</param>
        /// <param name="retryCount">The number of times to retry for.</param>
        /// <param name="strategy">The strategy to use for retrying. If null uses the DefaultStrategy.</param>
        /// <param name="retryOnError">Determines if a retry should be made based on the passed in exception. Defaults to retry on all exceptions.</param>
        /// <param name="scheduler">The scheduler to the output to.</param>
        /// <param name="log">The logger to output to.</param>
        /// <returns>The observable with exponential backoff retry support.</returns>
        public static IObservable<T> RetryWithBackoff<T>(
            this IObservable<T> source,
            int? retryCount = null,
            Func<int, TimeSpan> strategy = null,
            Func<Exception, bool> retryOnError = null,
            IScheduler scheduler = null,
            IFullLogger? log = default)
        {
            strategy ??= DefaultStrategy;
            scheduler ??= DefaultScheduler.Instance;
            retryOnError ??= _ => true;
            var attempt = 0;

            IObservable<Notification<T>> deferred = Observable.Defer(() =>
            {
                var num = attempt;
                attempt = num + 1;
                return (num == 0 ? source : source.DelaySubscription(strategy(attempt - 1), scheduler))
                    .Select(Notification.CreateOnNext)
                    .Catch((Func<Exception, IObservable<Notification<T>>>)
                        (ex =>
                        {
                            log?.Warn(ex, $"Retrying attempt: {attempt}");
                            return !retryOnError(ex)
                                ? Observable.Return(Notification.CreateOnError<T>(ex))
                                : Observable.Throw<Notification<T>>(ex);
                        }));
            });

            return (retryCount is null ? deferred.Retry() : deferred.Retry(retryCount.Value)).Dematerialize();
        }
    }
}