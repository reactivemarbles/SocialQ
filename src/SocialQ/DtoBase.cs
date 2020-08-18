using System;

namespace SocialQ
{
    public abstract class DtoBase
    {
        public DtoBase()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
    }
}