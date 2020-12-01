using System;

namespace SocialQ.Stores
{
    /// <summary>
    /// Represents a store over the wire.
    /// </summary>
    public class StoreDto : DtoBase
    {
        /// <summary>
        /// Gets the default <see cref="StoreDto"/>.
        /// </summary>
        public static StoreDto Default => new StoreDto
        {
            Id = Guid.NewGuid(),
            Name = "Default",
            Category = StoreCategory.Electronics,
            Coordinate = Coordinate.Default,
            AverageWait = TimeSpan.FromMinutes(30),
            CurrentWait = TimeSpan.FromMinutes(15),
            InStoreOperation = true,
            OpeningTime = new DateTimeOffset(
                2020,
                01,
                01,
                8,
                0,
                0,
                TimeSpan.Zero),
            CloseTime = new DateTimeOffset(
                2020,
                01,
                01,
                17,
                0,
                0,
                TimeSpan.Zero)
        };

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the opening time.
        /// </summary>
        public DateTimeOffset OpeningTime { get; set; }

        /// <summary>
        /// Gets or sets the close time.
        /// </summary>
        public DateTimeOffset CloseTime { get; set; }

        /// <summary>
        /// Gets or sets the current wait time.
        /// </summary>
        public TimeSpan CurrentWait { get; set; }

        /// <summary>
        /// Gets or sets average wait time.
        /// </summary>
        public TimeSpan AverageWait { get; set; }

        /// <summary>
        /// Gets or sets the coordinate.
        /// </summary>
        public Coordinate? Coordinate { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        public Address? Address { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        public string? Phone { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets in store service.
        /// </summary>
        public bool InStoreOperation { get; set; }

        /// <summary>
        /// Gets or sets the number of cases reported.
        /// </summary>
        public int CasesReported { get; set; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        public StoreCategory Category { get; set; }
    }
}