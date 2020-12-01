using System.Threading.Tasks;
using Shiny.Notifications;

namespace SocialQ.Forms
{
    /// <summary>
    /// Represents an <see cref="INotificationDelegate"/>.
    /// </summary>
    public class LocalNotificationDelegate : INotificationDelegate
    {
        private readonly IDialogs _dialogs;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalNotificationDelegate"/> class.
        /// </summary>
        /// <param name="dialogs">The dialogs.</param>
        public LocalNotificationDelegate(IDialogs dialogs) => _dialogs = dialogs;

        /// <inheritdoc/>
        public Task OnReceived(Notification notification) => null!;

        /// <inheritdoc/>
        public Task OnEntry(NotificationResponse response) => null!;
    }
}