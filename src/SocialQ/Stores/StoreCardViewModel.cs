using System;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace SocialQ.Stores
{
    /// <summary>
    /// Store card view model.
    /// </summary>
    public class StoreCardViewModel : ItemViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StoreCardViewModel"/> class.
        /// </summary>
        /// <param name="storeDto">The dto.</param>
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

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [Reactive] public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Reactive] public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the opening time.
        /// </summary>
        [Reactive] public DateTimeOffset OpeningTime { get; set; }

        /// <summary>
        /// Gets or sets the close time.
        /// </summary>
        [Reactive] public DateTimeOffset CloseTime { get; set; }

        /// <summary>
        /// Gets or sets the current wait time.
        /// </summary>
        [Reactive] public TimeSpan CurrentWait { get; set; }

        /// <summary>
        /// Gets or sets the average wait time.
        /// </summary>
        [Reactive] public TimeSpan AverageWait { get; set; }

        /// <summary>
        /// Gets or sets the coordinate.
        /// </summary>
        [Reactive] public Coordinate? Coordinate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether get or sets a value indicating whether there is in store service.
        /// </summary>
        [Reactive] public bool InStoreOperation { get; set; }

        /// <summary>
        /// Gets or sets the number of cases reported.
        /// </summary>
        [Reactive] public int CasesReported { get; set; }
    }
}