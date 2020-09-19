using System;
using System.Diagnostics;
using System.Reactive.Concurrency;
using ReactiveUI;
using Splat;

namespace SocialQ.Forms
{
    public class SocialQExceptionHandler : IObserver<Exception>, IEnableLogger
    {
        /// <inheritdoc />
        public void OnNext(Exception value)
        {
            if (Debugger.IsAttached)
            {
                Debugger.Break();
            }

            this.Log().Error(value, "Unhandled exception");
        }

        /// <inheritdoc />
        public void OnError(Exception error)
        {
            if (Debugger.IsAttached)
            {
                Debugger.Break();
            }

            this.Log().Error(error, "Unhandled exception");

            RxApp.MainThreadScheduler.Schedule(() => throw error);
        }

        /// <inheritdoc />
        public void OnCompleted()
        {
            if (Debugger.IsAttached)
            {
                Debugger.Break();
            }

            RxApp.MainThreadScheduler.Schedule(() => this.Log()?.Info($"The {nameof(SocialQExceptionHandler)} has completed!"));
        }
    }
}