using System;
using Akavache;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using Refit;
using Serilog;
using Sextant;
using Sextant.Plugins.Popup;
using Sextant.XamForms;
using SocialQ.Mocks.Queue;
using SocialQ.Mocks.Stores;
using SocialQ.Queue;
using SocialQ.Stores;
using Splat;

namespace SocialQ.Forms
{
    /// <summary>
    /// Extension methods for Microsoft Dependency Injection.
    /// </summary>
    public static class MicrosoftDependencyInjectionExtensions
    {
        /// <summary>
        /// Register a view model and a view for navigation.
        /// </summary>
        /// <param name="serviceCollection">The service collection.</param>
        /// <typeparam name="TView">The view type.</typeparam>
        /// <typeparam name="TViewModel">The view model type.</typeparam>
        /// <returns>The container collection.</returns>
        public static IServiceCollection RegisterForNavigation<TView, TViewModel>(this IServiceCollection serviceCollection)
            where TView : class, IViewFor<TViewModel>
            where TViewModel : ViewModelBase
        {
            serviceCollection.AddTransient<IViewFor<TViewModel>, TView>();
            serviceCollection.AddTransient<TViewModel>();
            return serviceCollection;
        }

        /// <summary>
        /// Registers <see cref="Sextant"/> to the container.
        /// </summary>
        /// <param name="serviceCollection">The service collection.</param>
        /// <returns>The container collection.</returns>
        public static IServiceCollection AddSextant(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IView>(provider => new NavigationView(RxApp.TaskpoolScheduler, RxApp.MainThreadScheduler, provider.GetService<IViewLocator>()!));
            serviceCollection.AddSingleton<IPopupViewStackService, PopupViewStackService>();
            serviceCollection.AddSingleton<IViewModelFactory, DefaultViewModelFactory>();
            return serviceCollection;
        }

        /// <summary>
        /// Register <see cref="Akavache"/> to the container.
        /// </summary>
        /// <param name="serviceCollection">The service collection.</param>
        /// <returns>The container collection.</returns>
        public static IServiceCollection AddAkavache(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton(BlobCache.LocalMachine);
            return serviceCollection;
        }

        /// <summary>
        /// Registers <see cref="Serilog"/> to the container.
        /// </summary>
        /// <param name="serviceCollection">The service collection.</param>
        /// <param name="factory">The logger factory.</param>
        /// <returns>The container collection.</returns>
        public static IServiceCollection AddSerilog(this IServiceCollection serviceCollection, Func<LoggerConfiguration> factory)
        {
            Log.Logger = factory().CreateLogger();
            var funcLogManager = new FuncLogManager(type =>
            {
                var actualLogger = global::Serilog.Log.ForContext(type);
                return new SerilogFullLogger(actualLogger);
            });

            serviceCollection.AddSingleton<ILogManager>(funcLogManager);

            return serviceCollection;
        }

        /// <summary>
        /// Register api contracts to the container.
        /// </summary>
        /// <param name="serviceCollection">The service collection.</param>
        /// <param name="useMocks">A value indicating whether or not to user mocks.</param>
        /// <returns>The container collection.</returns>
        public static IServiceCollection AddApiContracts(this IServiceCollection serviceCollection, bool useMocks)
        {
            if (useMocks)
            {
                serviceCollection.AddMockApiContracts();
            }
            else
            {
                serviceCollection.AddFunctionApiContracts();
            }

            return serviceCollection;
        }

        /// <summary>
        /// Register the azure function api contracts.
        /// </summary>
        /// <param name="serviceCollection">The service collection.</param>
        /// <returns>The container collection.</returns>
        public static IServiceCollection AddFunctionApiContracts(this IServiceCollection serviceCollection) =>
            serviceCollection
                .AddSingleton(RestService.For<IQueueApiContract>("https://socialq.azurewebsites.net"))
                .AddSingleton(RestService.For<IStoreApiContract>("https://socialq.azurewebsites.net"))
                .AddScoped(typeof(IHubClient<>), typeof(SignalRHubClientBase<>));

        /// <summary>
        /// Register the mock api contracts.
        /// </summary>
        /// <param name="serviceCollection">The service collection.</param>
        /// <returns>The container collection.</returns>
        public static IServiceCollection AddMockApiContracts(this IServiceCollection serviceCollection) =>
            serviceCollection
                .AddSingleton<IStoreApiContract, StoreApiContractMock>()
                .AddSingleton<IQueueApiContract, QueueApiContractMock>()
                .AddSingleton<IHubClient<QueuedStoreDto>, QueueHubClientMock>();

        /// <summary>
        /// Register the api clients.
        /// </summary>
        /// <param name="serviceCollection">The service collection.</param>
        /// <returns>The container collection.</returns>
        public static IServiceCollection AddApiClients(this IServiceCollection serviceCollection) =>
            serviceCollection
                .AddSingleton<IQueueApiClient, QueueApiClient>()
                .AddSingleton<IStoreApiClient, StoreApiClient>();

        /// <summary>
        /// Register data services.
        /// </summary>
        /// <param name="serviceCollection">The service collection.</param>
        /// <returns>The container collection.</returns>
        public static IServiceCollection AddDataServices(this IServiceCollection serviceCollection) =>
            serviceCollection
                .AddSingleton<IQueueService, QueueService>()
                .AddSingleton<IStoreService, StoreService>();
    }
}