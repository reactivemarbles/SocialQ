using System;
using System.Reactive;
using System.Reactive.Linq;
using Shiny.Notifications;

namespace SocialQ.Startup
{
    public class NotificationAccessOperation : IStartupOperation
    {
        private readonly INotificationManager _notificationManager;

        public NotificationAccessOperation(INotificationManager notificationManager)
        {
            _notificationManager = notificationManager;
        }

        // TODO: [rlittlesii: September 18, 2020] Should probably check the status.
        public IObservable<Unit> Start() =>
            Observable.FromAsync(() => _notificationManager.RequestAccess()).Select(_ => Unit.Default);

        public bool CanStart() => true;
    }
}