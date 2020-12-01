using System;
using System.Reactive.Linq;
using DynamicData;
using SocialQ.Stores;

namespace SocialQ.Queue
{
    /// <summary>
    /// Represents a <see cref="ServiceBase"/> for <see cref="QueuedStoreDto"/>.
    /// </summary>
    public class QueueService : ServiceBase, IQueueService
    {
        private readonly IQueueApiClient _apiClient;

        private readonly SourceCache<QueuedStoreDto, Guid> _queue =
            new SourceCache<QueuedStoreDto, Guid>(x => x.Id);

        /// <summary>
        /// Initializes a new instance of the <see cref="QueueService"/> class.
        /// </summary>
        /// <param name="apiClient">The api client.</param>
        public QueueService(IQueueApiClient apiClient) => _apiClient = apiClient;

        /// <inheritdoc/>
        public IObservableCache<QueuedStoreDto, Guid> Queue => _queue.AsObservableCache();

        /// <inheritdoc/>
        public IObservable<QueuedStoreDto> GetQueue(Guid userId, bool forceUpdate = true) =>
            _apiClient
                .GetQueue(userId, forceUpdate)
                .AddOrUpdate(_queue);

        /// <inheritdoc/>
        public IObservable<QueuedStoreDto> EnQueue(Guid userId, StoreDto store) => _apiClient
           .Enqueue(new EnqueueRequest(userId, store))
           .AddOrUpdate(_queue);

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _queue.Dispose();
            }
        }

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