using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using DynamicData;

namespace SocialQ
{
    public static class StoreServiceFunctions
    {
        public static IObservable<IEnumerable<StoreDto>> EditDiff(this IObservable<IEnumerable<StoreDto>> source, SourceCache<StoreDto, Guid> storeCache) =>
            Observable.Create<IEnumerable<StoreDto>>(observer =>
            {
                CompositeDisposable disposable = new CompositeDisposable();

                source
                    .Subscribe(stores => storeCache.EditDiff(stores, EqualityComparer<StoreDto>.Default))
                    .DisposeWith(disposable);

                storeCache
                    .Connect()
                    .RefCount()
                    .ToCollection()
                    .Subscribe(observer)
                    .DisposeWith(disposable);

                return disposable;
            });

        public static IObservable<StoreDto> AddOrUpdate(this IObservable<StoreDto> source, SourceCache<StoreDto, Guid> stores) =>
            Observable.Create<StoreDto>(observer =>
            {
                CompositeDisposable disposable = new CompositeDisposable();
                disposable.Add(source.Subscribe(stores.AddOrUpdate));
                disposable.Add(stores.Connect().RefCount().ToCollection().Subscribe((IObserver<IReadOnlyCollection<StoreDto>>) observer));
                return disposable;
            });
    }
}