using System;
using Akavache;

namespace SocialQ.Queue
{
    public interface IQueueApiClient
    {
        IBlobCache BlobCache { get; }

        IObservable<QueuedStoreDto> Enqueue(EnqueueRequest request);

        IObservable<QueuedStoreDto> GetQueue(Guid userId, bool forceUpdate = false);
    }
}