using System;
using System.Reactive;

namespace SocialQ.Startup
{
    public interface IAppStartup
    {
        IObservable<Unit> Startup();
    }
}