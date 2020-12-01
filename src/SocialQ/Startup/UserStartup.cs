using System;
using System.Reactive;
using System.Reactive.Linq;

namespace SocialQ.Startup
{
    /// <summary>
    /// Represents a user start up operation.
    /// </summary>
    public class UserStartup : IStartupOperation
    {
        private readonly ISettings _settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserStartup"/> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public UserStartup(ISettings settings) => _settings = settings;

        /// <inheritdoc/>
        public IObservable<Unit> Start()
        {
            if (_settings.UserId == Guid.Empty)
            {
                _settings.UserId = Guid.NewGuid();
            }

            return Observable.Return(Unit.Default);
        }

        /// <inheritdoc/>
        public bool CanStart() => true;
    }
}