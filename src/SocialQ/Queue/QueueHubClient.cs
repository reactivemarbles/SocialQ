using Microsoft.AspNetCore.SignalR.Client;

namespace SocialQ.Queue
{
    public class QueueHubClient : SignalRHubClientBase<QueuedStoreDto>
    {
        public QueueHubClient(HubConnection connection)
            : base(connection)
        {
        }
    }
}