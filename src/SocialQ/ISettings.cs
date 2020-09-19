using System;

namespace SocialQ
{
    public interface ISettings
    {
        Guid UserId { get; set; }
        string UserName { get; set; }
    }
}