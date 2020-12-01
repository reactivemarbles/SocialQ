using System;
using ReactiveUI;

namespace SocialQ.Forms
{
    /// <summary>
    /// Represents the settings.
    /// </summary>
    public class Settings : ReactiveObject, ISettings
    {
        private readonly Shiny.Settings.ISettings _settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="Settings"/> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public Settings(Shiny.Settings.ISettings settings) => _settings = settings;

        /// <inheritdoc/>
        public Guid UserId
        {
            get => _settings.Get(nameof(UserId), Guid.Empty);
            set
            {
                _settings.SetValue(nameof(UserId), value);
                this.RaisePropertyChanged();
            }
        }

        /// <inheritdoc/>
        public string UserName
        {
            get => _settings.Get(nameof(UserName), string.Empty);
            set
            {
                _settings.SetValue(nameof(UserName), value);
                this.RaisePropertyChanged();
            }
        }
    }
}