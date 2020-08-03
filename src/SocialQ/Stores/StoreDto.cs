using System;

namespace SocialQ
{
    public class StoreDto : DtoBase
    {
        public string Name { get; set; }

        public DateTimeOffset OpeningTime { get; set; }

        public DateTimeOffset CloseTime { get; set; }

        public TimeSpan CurrentWait { get; set; }

        public TimeSpan AverageWait { get; set; }

        public Coordinate Coordinate { get; set; }

        public bool InStoreOperation { get; set; }

        public int CasesReported { get; set; }

        public static StoreDto Default => new StoreDto
        {
            Id = Guid.NewGuid(),
            Name = "Default",
            Coordinate = SocialQ.Coordinate.Default,
            AverageWait = TimeSpan.FromMinutes(30),
            CurrentWait = TimeSpan.FromMinutes(15),
            InStoreOperation = true,
            OpeningTime = new DateTimeOffset(2020,01,01,8,0,0, TimeSpan.Zero),
            CloseTime = new DateTimeOffset(2020,01,01,17,0,0, TimeSpan.Zero)
        };
    }
}