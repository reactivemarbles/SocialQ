using Microsoft.AspNetCore.SignalR.Client;

namespace SocialQ
{
    /// <summary>
    /// Parameters for signal r connections.
    /// </summary>
    public class SignalRParameters
    {
        /// <summary>
        /// Gets the uri.
        /// </summary>
        public static string Url => "https://none.ya.biz";

        /// <summary>
        /// Gets the signal r hub client.
        /// </summary>
        public static HubConnection Client => new HubConnectionBuilder().WithUrl(Url).Build();
    }
}