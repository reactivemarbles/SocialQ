using System;
using System.Reactive;
using System.Reactive.Linq;
using Shiny.Notifications;

namespace SocialQ.Startup
{
    /// <summary>
    /// <see cref="IStartupOperation"/> that executes notification access.
    /// </summary>
    public class NotificationAccessOperation : IStartupOperation
    {
        private readonly INotificationManager _notificationManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationAccessOperation"/> class.
        /// </summary>
        /// <param name="notificationManager">The notification manager.</param>
        public NotificationAccessOperation(INotificationManager notificationManager) => _notificationManager = notificationManager;

        // TODO: [rlittlesii: September 18, 2020] Should probably check the status.

        /// <inheritdoc/>
        public IObservable<Unit> Start() => Observable.Create<Unit>(
            observer => Observable.FromAsync(() => _notificationManager.RequestAccess()).Select(_ => Unit.Default).Subscribe(observer));

        /// <inheritdoc/>
        public bool CanStart() => true;
    }
}