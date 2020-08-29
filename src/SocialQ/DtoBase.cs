using System;

namespace SocialQ
{
    public abstract class DtoBase
    {
        protected DtoBase()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
    }
}