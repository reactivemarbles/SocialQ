using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialQ.Stores
{
    public class StoreDto : DtoBase
    {
        public string Name { get; set; }

        public DateTimeOffset OpeningTime { get; set; }

        public DateTimeOffset CloseTime { get; set; }

        public TimeSpan CurrentWait { get; set; }

        public TimeSpan AverageWait { get; set; }

        public Coordinate Coordinate { get; set; }

        public Address Address { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }
        public bool InStoreOperation { get; set; }

        public int CasesReported { get; set; }

        public StoreCategory Category { get; set; }

        public static StoreDto Default => new StoreDto
        {
            Id = Guid.NewGuid(),
            Name = "Default",
            Category = StoreCategory.Electronics,
            Coordinate = Coordinate.Default,
            AverageWait = TimeSpan.FromMinutes(30),
            CurrentWait = TimeSpan.FromMinutes(15),
            InStoreOperation = true,
            OpeningTime = new DateTimeOffset(2020,01,01,8,0,0, TimeSpan.Zero),
            CloseTime = new DateTimeOffset(2020,01,01,17,0,0, TimeSpan.Zero)
        };
    }

    public class Address
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }

    public enum StoreCategory
    {
        Restaurant,
        
        Grocery,

        Sporting,

        Laundry,

        Furniture,

        HomeImprovement,

        Gym,

        Electronics
    }

    public static class StoreCategoryExtensions
    {
        public static List<T> ListEnumeration<T>()
            where T : Enum
            => ((T[]) Enum.GetValues(typeof(T))).ToList();
    }
}