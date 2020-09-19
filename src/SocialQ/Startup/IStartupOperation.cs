using System;
using System.Reactive;

namespace SocialQ.Startup
{
    public interface IStartupOperation
    {
        IObservable<Unit> Start();

        bool CanStart();
    }
}