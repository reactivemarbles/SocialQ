using System;
using System.ComponentModel;

namespace SocialQ
{
    /// <summary>
    /// Interface representing settings.
    /// </summary>
    public interface ISettings : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        string UserName { get; set; }
    }
}