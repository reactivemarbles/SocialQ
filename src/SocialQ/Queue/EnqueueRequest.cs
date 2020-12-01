using System;
using SocialQ.Stores;

namespace SocialQ.Queue
{
    /// <summary>
    /// Represents a request to queued a <see cref="StoreDto"/> to a user.
    /// </summary>
    public class EnqueueRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnqueueRequest"/> class.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="store">The store.</param>
        public EnqueueRequest(Guid userId, StoreDto store)
        {
            UserId = userId;
            Store = store;
        }

        /// <summary>
        /// Gets the user id.
        /// </summary>
        public Guid UserId { get; }

        /// <summary>
        /// Gets the store.
        /// </summary>
        public StoreDto Store { get; }
    }
}