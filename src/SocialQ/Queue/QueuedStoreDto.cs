using System;

namespace SocialQ.Queue
{
    public class QueuedStoreDto : DtoBase
    {
        public UserDto User { get; set; }

        public StoreDto Store { get; set; }

        public DateTimeOffset RemainingQueueTime { get; set; } = DateTimeOffset.UtcNow;
    }

    public class UserDto : DtoBase
    {
        public string UserId { get; set; }

        public string EmailAddress { get; set; }

        public DateTimeOffset Offset { get; set; } 
    }
}