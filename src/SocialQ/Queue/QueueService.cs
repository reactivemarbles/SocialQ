using System;
using System.Reactive.Linq;
using DynamicData;
using SocialQ.Stores;

namespace SocialQ.Queue
{
    public class QueueService : IQueueService
    {
        private readonly IQueueApiClient _apiClient;

        private readonly SourceCache<QueuedStoreDto, Guid> _queue =
            new SourceCache<QueuedStoreDto, Guid>(x => x.Id);

        public QueueService(IQueueApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public IObservableCache<QueuedStoreDto, Guid> Queue => _queue.AsObservableCache();

        public IObservable<QueuedStoreDto> GetQueue(Guid userId, bool forceUpdate = true) =>
            _apiClient
                .GetQueue(userId, forceUpdate)
                .AddOrUpdate(_queue);

        public IObservable<QueuedStoreDto> EnQueue(Guid userId, StoreDto store) =>
            _apiClient
                .Enqueue(new EnqueueRequest { UserId = userId, Store = store })
                .AddOrUpdate(_queue);

        private IObservable<QueuedStoreDto> UpdateQueueCache(QueuedStoreDto queuedStoreDto) =>
            Observable
                .Create<QueuedStoreDto>(observer =>
                    _queue
                        .Connect()
                        .RefCount()
                        .CacheChangeSet($"{nameof(GetQueue)}-{queuedStoreDto.Id}", _apiClient.BlobCache)
                        .Subscribe(_ => observer.OnNext(queuedStoreDto)));
    }
}