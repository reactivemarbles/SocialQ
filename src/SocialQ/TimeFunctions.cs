using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace SocialQ
{
    /// <summary>
    /// Functions that affect time.
    /// </summary>
    public static class TimeFunctions
    {
        private static readonly TimeSpan DefaultRefreshInterval = TimeSpan.FromSeconds(1);

        /// <summary>
        ///     Returns the remaining time from a provided start time.
        /// </summary>
        /// <param name="startTime">The start time.</param>
        /// <param name="scheduler">The thread scheduler to execute the timer.</param>
        /// <param name="refreshInterval">The refresh interval.</param>
        /// <returns>An observable of time.</returns>
        public static IObservable<TimeSpan> RemainingTime(
            this IObservable<DateTimeOffset> startTime,
            IScheduler scheduler,
            TimeSpan refreshInterval = default) =>
            startTime
                .Select(x =>
                {
                    var schedulerNow = x - scheduler.Now;
                    refreshInterval = refreshInterval == TimeSpan.Zero ? DefaultRefreshInterval : refreshInterval;
                    return Observable
                        .Interval(refreshInterval, scheduler)
                        .Scan(schedulerNow, (acc, _) => acc - refreshInterval)
                        .TakeUntil(timeSpan => timeSpan <= TimeSpan.FromSeconds(0));
                })
                .Switch();
    }
}