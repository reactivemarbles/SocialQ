using System;
using System.Collections.Generic;
using DynamicData;

namespace SocialQ.Stores
{
    /// <summary>
    /// Store service.
    /// </summary>
    public class StoreService : IStoreService, IDisposable
    {
        private readonly SourceCache<StoreDto, Guid> _stores =
            new SourceCache<StoreDto, Guid>(x => x.Id);

        private readonly SourceList<string?> _metadata = new SourceList<string?>();
        private readonly IStoreApiClient _apiClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="StoreService"/> class.
        /// </summary>
        /// <param name="apiClient">The api client.</param>
        public StoreService(IStoreApiClient apiClient) => _apiClient = apiClient;

        /// <inheritdoc/>
        public IObservableCache<StoreDto, Guid> Stores => _stores;

        /// <inheritdoc/>
        public IObservable<IChangeSet<string?>> Metadata => _metadata.Connect().RefCount();

        /// <inheritdoc/>
        public IObservable<IEnumerable<StoreDto>> GetStores(bool forceUpdate = true) => _apiClient
           .GetStores(forceUpdate)
           .AddOrUpdate(_stores);

        /// <inheritdoc/>
        public IObservable<IEnumerable<string?>> GetStoreMetadata(bool forceUpdate = true) => _apiClient
           .GetStoreMetadata(forceUpdate)
           .AddRange(_metadata);

        /// <inheritdoc/>
        public IObservable<StoreDto> GetStore(Guid id, bool forceUpdate = true) => _apiClient
           .GetStore(id, forceUpdate)
           .AddOrUpdate(_stores);

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose of resources.
        /// </summary>
        /// <param name="disposing">A value indicating whether disposal is in progress.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _stores?.Dispose();
                _metadata?.Dispose();
            }
        }
    }
}