using System;
using System.Reactive.Subjects;
using Akavache;
using NSubstitute;
using Splat;
using Xunit;

namespace SocialQ.UnitTests
{
    public class ExtensionsTests
    {
        [Fact]
        public void CacheApiResponseCallsBlobCache()
        {
            // Given
            var blobCache = Substitute.For<IBlobCache>();
            Subject<StoreDto> fixture = new Subject<StoreDto>();

            // When
            fixture.CacheApiResult(nameof(fixture), blobCache, Substitute.For<IFullLogger>()).Subscribe();

            // Then
            blobCache.Received().GetObject<StoreDto>(nameof(fixture));
        }
    }
}