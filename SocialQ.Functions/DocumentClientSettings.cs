using Microsoft.Azure.Documents.Client;

namespace SocialQ.Functions
{
    public class DocumentClientSettings
    {
        public string EndpointUrl { get; set; }
        public string AuthorizationKey { get; set; }
        public ConnectionPolicy ConnectionPolicy { get; set; }
    }
}