using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using DynamicData;

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
            _queue.AddOrUpdate(new List<QueuedStoreDto>
            {
                new QueuedStoreDto
                {
                    Id = Guid.Parse("8F89AC0A-C056-420A-8F59-12539AC5798D"),
                     Store = new StoreDto
                     {
                         Name = "Home Depot"
                             },
                    RemainingQueueTime = DateTimeOffset.Now.AddHours(1)
                },
                new QueuedStoreDto
                {
                    Id = Guid.Parse("E2ED6680-CA10-4AFE-8C83-7B916C22D3A9"),
                    Store = new StoreDto
                    {
                        Name = "Kroger"
                    },
                    RemainingQueueTime = DateTimeOffset.Now.AddHours(2)
                },
                new QueuedStoreDto
                {
                    Id = Guid.Parse("1DDABBAC-B2C0-44F9-A08E-2F09F266E9D7"),
                    Store = new StoreDto
                    {
                        Name = "Academy"
                    },
                    RemainingQueueTime = DateTimeOffset.Now.AddHours(3)
                }
            });
        }

        public IObservableCache<QueuedStoreDto, Guid> Queue => _queue.AsObservableCache();

        public IObservable<QueuedStoreDto> GetQueue(Guid userId, bool forceUpdate = true) =>
            _apiClient
                .GetQueue(userId, forceUpdate)
                .SelectMany(UpdateQueueCache);

        public IObservable<Unit> EnQueue(QueuedStoreDto dto) =>
            _apiClient
                .Enqueue(dto);

        public IObservable<Unit> UpdateQueue()
        {
            return null;
        }

        private IObservable<QueuedStoreDto> UpdateQueueCache(QueuedStoreDto queuedStoreDto) =>
            Observable
                .Create<QueuedStoreDto>(observer =>
                    _queue
                        .Connect()
                        .RefCount()
                        .CacheChangeSet($"{nameof(GetQueue)}-{queuedStoreDto.Id}", _apiClient.BlobCache)
                        .Subscribe(_ =>
                            observer
                                .OnNext(queuedStoreDto)));
    }
}