using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using SocialQ.Stores;

namespace SocialQ.Mocks.Stores
{
    /// <summary>
    /// Represents a mock <see cref="IStoreApiContract"/>.
    /// </summary>
    public class StoreApiContractMock : IStoreApiContract
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StoreApiContractMock"/> class.
        /// </summary>
        public StoreApiContractMock() => Items = new StoreDtoGenerator().Items;

        /// <summary>
        /// Gets or sets the items for the contract.
        /// </summary>
        public List<StoreDto> Items { get; set; }

        /// <inheritdoc/>
        public IObservable<StoreDto> GetStore(Guid storeId, FunctionParameters parameters) =>
            Observable
                .Return(Items.FirstOrDefault(x => x.Id == storeId))
                .Delay(TimeSpan.FromSeconds(1));

        /// <inheritdoc/>
        public IObservable<IEnumerable<StoreDto>> GetStores(FunctionParameters parameters) =>
            Observable
                .Return(Items)
                .Delay(TimeSpan.FromSeconds(5));

        /// <inheritdoc/>
        public IObservable<IEnumerable<string?>> GetMetadata(FunctionParameters parameters) =>
            Observable
                .Return(Items.Select(x => x.Name).Distinct())
                .Delay(TimeSpan.FromSeconds(3));
    }
}