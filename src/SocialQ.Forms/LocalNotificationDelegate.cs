using System.Threading.Tasks;
using Shiny.Notifications;

namespace SocialQ.Forms
{
    public class LocalNotificationDelegate : INotificationDelegate
    {
        private readonly IDialogs _dialogs;

        public LocalNotificationDelegate(IDialogs dialogs)
        {
            _dialogs = dialogs;
        }

        public Task OnReceived(Notification notification)
        {
            return null;
        }

        public Task OnEntry(NotificationResponse response)
        {
            return null;
        }
    }
}