using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using SocialQ.Stores;

namespace SocialQ.Mocks.Stores
{
    public class StoreApiContractMock : IStoreApiContract
    {
        public StoreApiContractMock()
        {
            Items = new StoreDtoGenerator().Items;
        }

        public List<StoreDto> Items { get; set; }

        public IObservable<StoreDto> GetStore(Guid storeId, FunctionParameters parameters) =>
            Observable
                .Return(Items.FirstOrDefault(x => x.Id == storeId))
                .Delay(TimeSpan.FromSeconds(1));

        public IObservable<IEnumerable<StoreDto>> GetStores(FunctionParameters parameters) =>
            Observable
                .Return(Items)
                .Delay(TimeSpan.FromSeconds(10));

        public IObservable<IEnumerable<string>> GetMetadata(FunctionParameters parameters) =>
            Observable
                .Return(Items.Select(x => x.Name).Distinct())
                .Delay(TimeSpan.FromSeconds(3));
    }
}