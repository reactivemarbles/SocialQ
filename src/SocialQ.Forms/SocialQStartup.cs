using System;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using Sextant;
using Shiny;
using SocialQ.ViewModels;
using Splat.Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;

namespace SocialQ.Forms
{
    public class SocialQStartup : ShinyStartup
    {
        public override void ConfigureServices(IServiceCollection services)
        {

            RxApp.DefaultExceptionHandler = new SocialQExceptionHandler();


            services.UseNotifications(); // set true
            services
                .AddSextant()
                .AddAkavache()
                .AddSerilog()
                .RegisterForNavigation<MainPage, MainViewModel>()
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