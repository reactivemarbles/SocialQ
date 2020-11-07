using System;
using System.ComponentModel;

namespace SocialQ
{
    public interface ISettings : INotifyPropertyChanged
    {
        Guid UserId { get; set; }
        string UserName { get; set; }
    }
}