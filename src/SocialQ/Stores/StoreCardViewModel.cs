using System;
using ReactiveUI;

namespace SocialQ.Stores
{
    public class StoreCardViewModel : ReactiveObject
    {
        private Coordinate _coordinate;
        private Guid _id;
        private string _name;
        private DateTimeOffset _openingTime;
        private DateTimeOffset _closeTime;
        private TimeSpan _currentWait;
        private TimeSpan _averageWait;
        private bool _inStoreOperation;
        private int _casesReported;

        public StoreCardViewModel(StoreDto storeDto)
        {
            Id = storeDto.Id;
            Name = storeDto.Name;
            OpeningTime = storeDto.OpeningTime;
            CloseTime = storeDto.CloseTime;
            CurrentWait = storeDto.CurrentWait;
            AverageWait = storeDto.AverageWait;
            InStoreOperation = storeDto.InStoreOperation;
            CasesReported = storeDto.CasesReported;
        }

        public Guid Id 
        {
            get => _id;
            set => this.RaiseAndSetIfChanged(ref _id, value);
        }

        public string Name 
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }

        public DateTimeOffset OpeningTime 
        {
            get => _openingTime;
            set => this.RaiseAndSetIfChanged(ref _openingTime, value);
        }
        public DateTimeOffset CloseTime 
        {
            get => _closeTime;
            set => this.RaiseAndSetIfChanged(ref _closeTime, value);
        }
        public TimeSpan CurrentWait 
        {
            get => _currentWait;
            set => this.RaiseAndSetIfChanged(ref _currentWait, value);
        }

        public TimeSpan AverageWait 
        {
            get => _averageWait;
            set => this.RaiseAndSetIfChanged(ref _averageWait, value);
        }

        public Coordinate Coordinate
        {
            get => _coordinate;
            set => this.RaiseAndSetIfChanged(ref _coordinate, value);
        }

        public bool InStoreOperation
        {
            get => _inStoreOperation;
            set => this.RaiseAndSetIfChanged(ref _inStoreOperation, value);
        }

        public int CasesReported
        {
            get => _casesReported;
            set => this.RaiseAndSetIfChanged(ref _casesReported, value);
        }
    }
}