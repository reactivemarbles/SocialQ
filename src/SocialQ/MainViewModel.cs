using Akavache;
using Sextant;
using Sextant.Plugins.Popup;
using Shiny.Notifications;

namespace SocialQ
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IPopupViewStackService _popupViewStackService;
        private readonly INotificationManager _notificationManager;
        private readonly IBlobCache _blobCache;

        public MainViewModel(
            IPopupViewStackService popupViewStackService,
            INotificationManager notificationManager,
            IBlobCache blobCache)
            : base(popupViewStackService)
        {
            _popupViewStackService = popupViewStackService;
            _notificationManager = notificationManager;
            _blobCache = blobCache;
        }
    }
}