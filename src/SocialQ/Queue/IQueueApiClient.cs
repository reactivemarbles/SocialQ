using System;
using System.Reactive;

namespace SocialQ.Queue
{
    public interface IQueueApiClient
    {
        IObservable<Unit> AddToQueue()
    }
}