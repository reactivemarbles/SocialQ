using System;
using Akavache;
using Splat;

namespace SocialQ.Queue
{
    public class QueueApiClient : IQueueApiClient
    {
        private readonly IQueueApiContract _apiContract;
        private readonly IHubClient<QueuedStoreDto> _hubClient;
        private readonly IFullLogger _logger;

        public QueueApiClient(IQueueApiContract apiContract, IHubClient<QueuedStoreDto> hubClient, IBlobCache blobCache, ILogManager logger)
        {
            _apiContract = apiContract;
            _hubClient = hubClient;
            BlobCache = blobCache;
            _logger = logger.GetLogger(GetType());
        }

        public IBlobCache BlobCache { get; }

        public IObservable<QueuedStoreDto> Enqueue(EnqueueRequest request) =>
            _apiContract
                .Enqueue(request, FunctionParameters.Default);

        public IObservable<QueuedStoreDto> GetQueue(Guid userId, bool forceUpdate = false) =>
            _hubClient
                .Connect($"{nameof(QueueApiClient)}-{userId}");
    }
}