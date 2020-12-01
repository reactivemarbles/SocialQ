using System;

namespace SocialQ
{
    /// <summary>
    /// Represents a base service.
    /// </summary>
    public class ServiceBase : IService, IDisposable
    {
        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes of the resources.
        /// </summary>
        /// <param name="disposing">A value indicating whether the instance is disposing.</param>
        protected virtual void Dispose(bool disposing)
        {
        }
    }
}