using System;
using Akavache;
using Splat;

namespace SocialQ.Queue
{
    /// <summary>
    /// Represents an api client that handles the <see cref="IQueueApiContract"/>.
    /// </summary>
    public class QueueApiClient : IQueueApiClient
    {
        private readonly IQueueApiContract _apiContract;
        private readonly IHubClient<QueuedStoreDto> _hubClient;
        private readonly IFullLogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueueApiClient"/> class.
        /// </summary>
        /// <param name="apiContract">The api contract.</param>
        /// <param name="hubClient">The hub client.</param>
        /// <param name="blobCache">The cache.</param>
        /// <param name="logger">The logger.</param>
        public QueueApiClient(IQueueApiContract apiContract, IHubClient<QueuedStoreDto> hubClient, IBlobCache blobCache, ILogManager logger)
        {
            _apiContract = apiContract;
            _hubClient = hubClient;
            BlobCache = blobCache;
            _logger = logger.GetLogger(GetType());
        }

        /// <inheritdoc/>
        public IBlobCache BlobCache { get; }

        /// <inheritdoc/>
        public IObservable<QueuedStoreDto> Enqueue(EnqueueRequest request) =>
            _apiContract
                .Enqueue(request, FunctionParameters.Default);

        /// <inheritdoc/>
        public IObservable<QueuedStoreDto> GetQueue(Guid userId, bool forceUpdate = false) =>
            _hubClient
                .Connect($"{nameof(QueueApiClient)}-{userId}");
    }
}