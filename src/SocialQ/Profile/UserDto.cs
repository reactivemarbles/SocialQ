using System;

namespace SocialQ.Queue
{
    /// <summary>
    /// Represents a user data transfer object.
    /// </summary>
    public class UserDto : DtoBase
    {
        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        public string? UserId { get; set; }

        /// <summary>
        /// Gets or sets the user email address.
        /// </summary>
        public string? EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the user date time offset.
        /// </summary>
        public DateTimeOffset Offset { get; set; }
    }
}