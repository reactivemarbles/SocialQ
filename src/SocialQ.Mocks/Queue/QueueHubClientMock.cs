using System;
using System.Reactive.Linq;
using SocialQ.Queue;

namespace SocialQ.Mocks.Queue
{
    public class QueueHubClientMock : HubClientMock<QueuedStoreDto>
    {
        public QueueHubClientMock()
        {
            Hub = Observable.Interval(TimeSpan.FromSeconds(8)).Select(x => new QueuedStoreDto());
        }

        public override IObservable<QueuedStoreDto> Hub { get; }
    }
}