using System;
using System.Collections.Generic;
using System.Reactive;
using DynamicData;

namespace SocialQ.Queue
{
    public interface IQueueService
    {
        IObservableCache<QueuedStoreDto, Guid> Queue { get; }

        IObservable<QueuedStoreDto> GetQueue(Guid userId, bool forceUpdate = true);

        IObservable<QueuedStoreDto> EnQueue(Guid userId, Guid storeId);
    }
}