using Microsoft.AspNetCore.SignalR.Client;

namespace SocialQ.Queue
{
    /// <summary>
    /// Represents a <see cref="SignalRHubClientBase{T}"/> for <see cref="QueuedStoreDto"/>.
    /// </summary>
    public class QueueHubClient : SignalRHubClientBase<QueuedStoreDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueueHubClient"/> class.
        /// </summary>
        /// <param name="connection">The hub connection.</param>
        public QueueHubClient(HubConnection connection)
            : base(connection)
        {
        }
    }
}