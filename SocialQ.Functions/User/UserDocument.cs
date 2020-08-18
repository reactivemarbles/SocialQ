using System;

namespace SocialQ.Functions.User
{
    public class UserDocument : DocumentBase
    {
        public Guid UserId { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public DateTimeOffset TimeOffset { get; set; }
    }
}