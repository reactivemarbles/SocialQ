using System;
using Akavache;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using Refit;
using Serilog;
using Sextant;
using Shiny;
using SocialQ.ViewModels;
using SocialQ.ViewModels.Stores;
using Splat.Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;

namespace SocialQ.Forms
{
    public class SocialQStartup : ShinyStartup
    {
        public override void ConfigureServices(IServiceCollection services)
        {

            RxApp.DefaultExceptionHandler = new SocialQExceptionHandler();
            BlobCache.ApplicationName = $"{nameof(SocialQ)}";

            services.UseNotifications(); // set true
            services
                .AddSerilog(() => new LoggerConfiguration())
                .AddSextant()
                .AddAkavache()
                .RegisterForNavigation<MainPage, MainViewModel>()
                .RegisterForNavigation<StoreSearch, StoreSearchViewModel>()
                .AddSingleton<IStoreService, StoreService>()
                .AddSingleton<IStoreApiClient, StoreApiClient>()
                .AddSingleton(RestService.For<IStoreApiContract>("https://socialq.azurewebsites.net"))
                .UseMicrosoftDependencyResolver();
        }

        public static Page NavigateToStart<T>()
            where T : IViewModel
        {
           ShinyHost.Container.GetService<IParameterViewStackService>().PushPage<T>().Subscribe();
            return (Page) ShinyHost.Container.GetService<IView>();
        }
    }
}