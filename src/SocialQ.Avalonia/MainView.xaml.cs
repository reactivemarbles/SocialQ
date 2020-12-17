using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;

namespace SocialQ.Avalonia
{
    public sealed class MainView : ReactiveWindow<MainViewModel>
    {
        public Button SwitchThemeButton => this.FindControl<Button>("SwitchThemeButton");
        
        public MainView()
        {
            this.WhenActivated(disposables => { });
            AvaloniaXamlLoader.Load(this);
        }
    }
}