using System;

namespace SocialQ.Functions.Queue
{
    public class QueueDocument : DocumentBase
    {
        public Guid Store { get; set; }

        public Guid User { get; set; }
    }
}