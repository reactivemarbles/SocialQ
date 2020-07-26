using System;
using System.Diagnostics;
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
            ////RxApp.MainThreadScheduler.Schedule(() => { throw value; });
        }

        /// <inheritdoc />
        public void OnError(Exception error)
        {
            if (Debugger.IsAttached)
            {
                Debugger.Break();
            }

            this.Log().Error(error, "Unhandled exception");
            ////RxApp.MainThreadScheduler.Schedule(() => { throw error; });
        }

        /// <inheritdoc />
        public void OnCompleted()
        {
            if (Debugger.IsAttached)
            {
                Debugger.Break();
            }
        }
    }
}