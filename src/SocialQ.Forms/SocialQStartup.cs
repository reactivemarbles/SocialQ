using System;
using Akavache;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using Refit;
using Serilog;
using Sextant;
using Sextant.Plugins.Popup;
using Shiny;
using SocialQ.Forms.Queue;
using SocialQ.Forms.Stores;
using SocialQ.Mocks.Queue;
using SocialQ.Queue;
using Splat;
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
                .RegisterForNavigation<Queues, QueuesViewModel>()
                .RegisterForNavigation<StoreSearch, StoreSearchViewModel>()
                .RegisterForNavigation<StoreDetail, StoreDetailViewModel>()
                .AddApiContracts(true)
                .AddApiClients()
                .AddDataServices()
                .AddSingleton(SignalRParameters.Client)
                .UseMicrosoftDependencyResolver();
        }

        public static Page NavigateToStart<T>()
            where T : IViewModel
        {
           Locator.Current.GetService<IParameterViewStackService>().PushPage<T>(resetStack: true, animate: false).Subscribe();
            return (NavigationPage) Locator.Current.GetService<IView>();
        }
    }
}