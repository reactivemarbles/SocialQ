using System;
using SocialQ.Functions.Store;
using SocialQ.Functions.User;

namespace SocialQ.Functions.Queue
{
    public class QueueProjection : DocumentBase
    {
        public StoreDocument Store { get; set; }

        public UserDocument User { get; set; }

        public DateTimeOffset RemainingQueueTime { get; set; } = DateTimeOffset.UtcNow;
    }
}