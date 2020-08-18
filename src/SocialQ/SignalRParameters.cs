using Microsoft.AspNetCore.SignalR.Client;

namespace SocialQ
{
    public class SignalRParameters
    {
        public static string Url => "https://none.yay.biz";

        public static HubConnection Client => new HubConnectionBuilder().WithUrl(Url).Build();
    }
}