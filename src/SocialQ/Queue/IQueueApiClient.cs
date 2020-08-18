using System;
using System.Reactive;
using Akavache;

namespace SocialQ.Queue
{
    public interface IQueueApiClient
    {
        IBlobCache BlobCache { get; }

        IObservable<Unit> Enqueue(QueuedStoreDto dto);

        IObservable<QueuedStoreDto> GetQueue(Guid userId, bool forceUpdate = false);
    }
}