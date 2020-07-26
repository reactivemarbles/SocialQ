using Akavache;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using Sextant;
using Sextant.Abstractions;
using Sextant.XamForms;
using SocialQ.ViewModels;
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

        public static IServiceCollection AddSerilog(this IServiceCollection serviceCollection)
        {
            var funcLogManager = new FuncLogManager(type =>
            {
                var actualLogger = global::Serilog.Log.ForContext(type);
                return new SerilogFullLogger(actualLogger);
            });

            serviceCollection.AddSingleton<ILogManager>(funcLogManager);

            return serviceCollection;
        }
    }
}