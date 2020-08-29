using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using LanguageExt;
using SocialQ.Queue;

namespace SocialQ.Mocks.Queue
{
    public class QueueHubClientMock : HubClientMock<QueuedStoreDto>
    {
        public QueueHubClientMock()
        {
            Items.AddRange(new[]
            {
                new QueuedStoreDto
                {
                    Id = Guid.Parse("8F89AC0A-C056-420A-8F59-12539AC5798D"),
                    Store = new StoreDto
                    {
                        Name = "Home Depot"
                    },
                    RemainingQueueTime = DateTimeOffset.Now.AddHours(1)
                },
                new QueuedStoreDto
                {
                    Id = Guid.Parse("E2ED6680-CA10-4AFE-8C83-7B916C22D3A9"),
                    Store = new StoreDto
                    {
                        Name = "Kroger"
                    },
                    RemainingQueueTime = DateTimeOffset.Now.AddHours(2)
                },
                new QueuedStoreDto
                {
                    Id = Guid.Parse("1DDABBAC-B2C0-44F9-A08E-2F09F266E9D7"),
                    Store = new StoreDto
                    {
                        Name = "Academy"
                    },
                    RemainingQueueTime = DateTimeOffset.Now.AddHours(3)
                }
            });
        }
    }
}