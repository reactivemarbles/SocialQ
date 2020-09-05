using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

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
            Observable.Return(Items.FirstOrDefault(x => x.Id == storeId));

        public IObservable<IEnumerable<StoreDto>> GetStores(FunctionParameters parameters) =>
            Observable.Return(Items)
                .Delay(TimeSpan.FromSeconds(3));

        public IObservable<IEnumerable<string>> GetMetadata(FunctionParameters parameters) =>
            Observable.Return(Items.Select(x => x.Name).Distinct());
    }
}