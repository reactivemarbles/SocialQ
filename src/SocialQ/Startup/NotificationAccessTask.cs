using System;
using System.Reactive;
using System.Reactive.Linq;
using Shiny.Notifications;
using SocialQ.Splash;

namespace SocialQ.Startup
{
    public class NotificationAccessTask : IStartupTask
    {
        private readonly INotificationManager _notificationManager;

        public NotificationAccessTask(INotificationManager notificationManager)
        {
            _notificationManager = notificationManager;
        }

        // TODO: [rlittlesii: September 18, 2020] Should probably check the status.
        public IObservable<Unit> Start() =>
            Observable.FromAsync(() => _notificationManager.RequestAccess()).Select(_ => Unit.Default);

        public bool CanStart() => true;
    }
}