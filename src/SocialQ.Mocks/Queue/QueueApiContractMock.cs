using System;
using System.Reactive;
using System.Reactive.Linq;
using SocialQ.Queue;

namespace SocialQ.Mocks.Queue
{
    /// <summary>
    /// Represents a mock <see cref="IQueueApiContract"/>.
    /// </summary>
    public class QueueApiContractMock : IQueueApiContract
    {
        /// <inheritdoc/>
        public IObservable<QueuedStoreDto> Enqueue(EnqueueRequest enqueueRequest, FunctionParameters parameters) =>
            Observable.Return(new QueuedStoreDto
            {
                Store = enqueueRequest.Store,
                User = new UserDto { Id = enqueueRequest.UserId },
                RemainingQueueTime = DateTimeOffset.Now.AddHours(1.5)
            });
    }
}