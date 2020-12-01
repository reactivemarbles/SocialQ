using Akavache;
using Sextant;
using Sextant.Plugins.Popup;
using Shiny.Notifications;

namespace SocialQ
{
    /// <summary>
    /// Main ViewModel.
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private readonly IPopupViewStackService _popupViewStackService;
        private readonly INotificationManager _notificationManager;
        private readonly IBlobCache _blobCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        /// <param name="popupViewStackService">The popup view stack service.</param>
        /// <param name="notificationManager">The notification manager.</param>
        /// <param name="blobCache">The blob cache.</param>
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