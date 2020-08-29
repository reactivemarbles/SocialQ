using System;
using Akavache;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using Serilog;
using Sextant;
using Sextant.Abstractions;
using Sextant.XamForms;
using SocialQ.Mocks.Queue;
using SocialQ.Queue;
using Splat;
using ILogger = Shiny.Logging.ILogger;

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
            serviceCollection.AddSingleton<IView, NavigationView>();
            serviceCollection.AddSingleton<IParameterViewStackService, ParameterViewStackService>();
            serviceCollection.AddSingleton<IViewModelFactory, DefaultViewModelFactory>();
            return serviceCollection;
        }
        
        public static IServiceCollection AddAkavache(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IBlobCache>(BlobCache.LocalMachine);
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

        public static IServiceCollection AddClients(this IServiceCollection serviceCollection, bool useMocks)
        {
            if (useMocks)
            {
                serviceCollection.AddMockApiClients();
            }
            else
            {
                serviceCollection.AddApiClients();
            }

            return serviceCollection;
        }


        public static IServiceCollection AddApiClients(this IServiceCollection serviceCollection)
        {
            return serviceCollection;
        }

        public static IServiceCollection AddMockApiClients(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IHubClient<QueuedStoreDto>>(new QueueHubClientMock());
            return serviceCollection;
        }
    }
}