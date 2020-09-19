using System;
using ReactiveUI;

namespace SocialQ.Forms
{
    public class Settings : ReactiveObject, ISettings
    {
        private readonly Shiny.Settings.ISettings _settings;

        public Settings(Shiny.Settings.ISettings settings)
        {
            _settings = settings;
        }

        public Guid UserId
        {
            get => _settings.Get( nameof(UserId), Guid.Empty);
            set
            {
                _settings.SetValue(nameof(UserId), value);
                this.RaisePropertyChanged();
            }
        }

        public string UserName
        {
            get => _settings.Get( nameof(UserName), string.Empty);
            set
            {
                _settings.SetValue(nameof(UserName), value);
                this.RaisePropertyChanged();
            }
        }
    }
}