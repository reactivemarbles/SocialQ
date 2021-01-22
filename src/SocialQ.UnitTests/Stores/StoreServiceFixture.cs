using System.Collections.Generic;
using System.Reactive.Linq;
using NSubstitute;
using ReactiveUI.Testing;
using SocialQ.Stores;

namespace SocialQ.UnitTests.Stores
{
    public class StoreServiceFixture : IBuilder
    {
        private IStoreApiClient _client;

        public StoreServiceFixture()
        {
            _client = Substitute.For<IStoreApiClient>();
            _client
                .GetStores(Arg.Any<bool>())
                .Returns(Observable.Return(new List<StoreDto> { StoreDto.Default }));
        }

        public StoreServiceFixture WithClient(IStoreApiClient client) => this.With(out _client, client);

        public static implicit operator StoreService(StoreServiceFixture fixture) => fixture.Build();

        private StoreService Build() => new StoreService(_client);
    }
}