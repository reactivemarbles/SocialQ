using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace SocialQ.Functions.Store
{
    public static class HttpRequestFunctions
    {
        public static async Task<T> Convert<T>(this HttpRequest httpRequest)
        {
            var content = await new StreamReader(httpRequest.Body).ReadToEndAsync().ConfigureAwait(false);

            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}