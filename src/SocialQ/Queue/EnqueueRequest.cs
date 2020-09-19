using System;

namespace SocialQ.Queue
{
    public class EnqueueRequest
    {
        public Guid UserId { get; set; }

        public StoreDto Store { get; set; }
    }
}