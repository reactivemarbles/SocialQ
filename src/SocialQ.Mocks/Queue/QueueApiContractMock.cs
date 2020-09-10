using System;
using System.Reactive;
using System.Reactive.Linq;
using SocialQ.Queue;

namespace SocialQ.Mocks.Queue
{
    public class QueueApiContractMock : IQueueApiContract
    {
        public IObservable<QueuedStoreDto> Enqueue(EnqueueRequest enqueueRequest, FunctionParameters parameters) =>
            Observable.Return(default(QueuedStoreDto));
    }
}