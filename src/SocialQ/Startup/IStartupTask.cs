using System;
using System.Reactive;

namespace SocialQ.Splash
{
    public interface IStartupTask
    {
        IObservable<Unit> Start();

        bool CanStart();
    }
}