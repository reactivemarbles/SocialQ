using System;
using System.Collections.Generic;
using System.Reactive;
using Refit;

namespace SocialQ.Queue
{
    public interface IQueueApiContract
    {
        /// <summary>
        /// Gets an store with the specified ID.
        /// </summary>
        /// <param name="enqueueRequest"></param>
        /// <param name="parameters">The azure function parameters.</param>
        /// <returns>An observable which signals with the store.</returns>
        [Post("/api/queue/add")]
        IObservable<Unit> Enqueue([Body(BodySerializationMethod.Serialized)] EnqueueRequest enqueueRequest, [Query] FunctionParameters parameters);
    }
}