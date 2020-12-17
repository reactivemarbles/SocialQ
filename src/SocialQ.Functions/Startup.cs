using System;
using System.IO;
using CosmosDbRepository;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using SocialQ.Functions;
using SocialQ.Functions.Store;

[assembly: FunctionsStartup(typeof(Startup))]
namespace SocialQ.Functions
{
    /// <summary>
    /// Starts the application.
    /// </summary>
    public class Startup : FunctionsStartup
    {
        /// <inheritdoc/>
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            builder
                .Services
                .Configure<DocumentClientSettings>(configuration.GetSection(nameof(DocumentClientSettings)))
                .AddOptions();

            builder
                .Services
                .AddTransient(provider => provider.AddClient());

            builder
                .Services
                .AddTransient(provider => provider.BuildCosmosDb());
        }
    }

    public static class ServiceProviderExtensions
    {
        public static DocumentClient AddClient(this IServiceProvider serviceProvider) =>
            serviceProvider.GetRequiredService<IOptions<DocumentClientSettings>>().Value.CreateClient();

        public static DocumentClient CreateClient(this DocumentClientSettings settings) =>
            new DocumentClient(settings.ToUri(), settings.AuthorizationKey, settings.ConnectionPolicy );

        public static Uri ToUri(this DocumentClientSettings settings) => new Uri(settings.EndpointUrl);

        public static ICosmosDb BuildCosmosDb(this IServiceProvider serviceProvider) =>
            new CosmosDbBuilder()
                .WithId("SocialQ")
                .WithDefaultThroughput(400)
                .AddCollection<StoreDocument>("Stores")
                .Build(serviceProvider.GetService<DocumentClient>());
    }
}