using System;
using System.Reactive;
using System.Reactive.Linq;

namespace SocialQ.Splash
{
    public class UserStartup : IStartupTask
    {
        private readonly ISettings _settings;

        public UserStartup(ISettings settings)
        {
            _settings = settings;
        }

        public IObservable<Unit> Start()
        {
            if (_settings.UserId == Guid.Empty)
            {
                _settings.UserId = Guid.NewGuid();
            }

            return Observable.Return(Unit.Default);
        }

        public bool CanStart() => true;
    }
}