using System;
using Akavache;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using Refit;
using Serilog;
using Sextant;
using Sextant.XamForms;
using SocialQ.Mocks.Queue;
using SocialQ.Mocks.Stores;
using SocialQ.Queue;
using Splat;

namespace SocialQ.Forms
{
    public static class MicrosoftDependencyInjectionExtensions
    {
        public static IServiceCollection RegisterForNavigation<TView, TViewModel>(this IServiceCollection collection)
            where TView : class, IViewFor<TViewModel>
            where TViewModel : ViewModelBase
        {
            collection.AddTransient<IViewFor<TViewModel>, TView>();
            collection.AddTransient<TViewModel>();
            return collection;
        }
        
        public static IServiceCollection AddSextant(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IView>(provider => new NavigationView(RxApp.TaskpoolScheduler, RxApp.MainThreadScheduler, provider.GetService<IViewLocator>()));
            serviceCollection.AddSingleton<IViewLocator, DefaultViewLocator>();
            serviceCollection.AddSingleton<IParameterViewStackService, ParameterViewStackService>();
            serviceCollection.AddSingleton<IViewModelFactory, DefaultViewModelFactory>();
            return serviceCollection;
        }
        
        public static IServiceCollection AddAkavache(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton(BlobCache.LocalMachine);
            return serviceCollection;
        }

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

        public static IServiceCollection AddFunctionApiContracts(this IServiceCollection serviceCollection) =>
            serviceCollection
                .AddSingleton(RestService.For<IQueueApiContract>("https://socialq.azurewebsites.net"))
                .AddSingleton(RestService.For<IStoreApiContract>("https://socialq.azurewebsites.net"))
                .AddScoped(typeof(IHubClient<>), typeof(SignalRHubClientBase<>));

        public static IServiceCollection AddMockApiContracts(this IServiceCollection serviceCollection) =>
            serviceCollection
                .AddSingleton<IStoreApiContract, StoreApiContractMock>()
                .AddSingleton<IQueueApiContract, QueueApiContractMock>()
                .AddSingleton<IHubClient<QueuedStoreDto>, QueueHubClientMock>();

        public static IServiceCollection AddApiClients(this IServiceCollection serviceCollection) =>
            serviceCollection
                .AddSingleton<IQueueApiClient, QueueApiClient>()
                .AddSingleton<IStoreApiClient, StoreApiClient>();

        public static IServiceCollection AddDataServices(this IServiceCollection serviceCollection) =>
            serviceCollection
                .AddSingleton<IQueueService, QueueService>()
                .AddSingleton<IStoreService, StoreService>();
    }
}