using System;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace SocialQ.UnitTests.Stores
{
    public class StoreServiceTests
    {
        [Fact]
        public void GetStoresReturnsItem()
        {
            // Given
            StoreService sut = new StoreServiceFixture();

            // When
            sut.GetStores().Subscribe();

            // Then
            sut.Stores.Items.Should().ContainSingle();
        }

        [Fact]
        public void GetStoresCallsClient()
        {
            // Given
            var client = Substitute.For<IStoreApiClient>();
            StoreService sut = new StoreServiceFixture().WithClient(client);

            // When
            sut.GetStores().Subscribe();

            // Then
            client.Received().GetStores(Arg.Any<bool>());
        }
    }
}