using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;

namespace SocialQ.Functions.Store
{
    public class StoreDocumentGenerator
    {
        private readonly IEnumerable<StoreNameCategory> _stores = new List<StoreNameCategory>
        {
            new StoreNameCategory { Name = "Academy", Category = StoreCategory.Sporting},
            new StoreNameCategory { Name = "Target", Category = StoreCategory.Grocery},
            new StoreNameCategory { Name = "Walmart", Category = StoreCategory.Grocery},
            new StoreNameCategory { Name = "Home Depot", Category = StoreCategory.HomeImprovement},
            new StoreNameCategory { Name = "Lowes", Category = StoreCategory.HomeImprovement},
            new StoreNameCategory { Name = "HEB", Category = StoreCategory.Grocery},
            new StoreNameCategory { Name = "Randall's", Category = StoreCategory.Grocery},
            new StoreNameCategory { Name = "Sam's Club", Category = StoreCategory.Grocery},
            new StoreNameCategory { Name = "Costco", Category = StoreCategory.Grocery},
            new StoreNameCategory { Name = "Aldi", Category = StoreCategory.Grocery},
            new StoreNameCategory { Name = "H-Mart", Category = StoreCategory.Grocery},
            new StoreNameCategory { Name = "Torchy's Tacos", Category = StoreCategory.Restaurant},
            new StoreNameCategory { Name = "Velvet Taco", Category = StoreCategory.Restaurant},
            new StoreNameCategory { Name = "CVS", Category = StoreCategory.Grocery},
            new StoreNameCategory { Name = "Fudruckers", Category = StoreCategory.Restaurant},
            new StoreNameCategory { Name = "Thai Lao Market", Category = StoreCategory.Restaurant},
            new StoreNameCategory { Name = "Ikea", Category = StoreCategory.Furniture},
            new StoreNameCategory { Name = "Mattress Firm", Category = StoreCategory.Furniture},
            new StoreNameCategory { Name = "Rooms to Go", Category = StoreCategory.Furniture},
            new StoreNameCategory { Name = "Mattress Firm", Category = StoreCategory.Furniture},
            new StoreNameCategory { Name = "Toasted Yolk", Category = StoreCategory.Grocery},
            new StoreNameCategory { Name = "LA Fitness", Category = StoreCategory.Gym},
            new StoreNameCategory { Name = "Apple", Category = StoreCategory.Electronics},
            new StoreNameCategory { Name = "AT&T", Category = StoreCategory.Electronics},
            new StoreNameCategory { Name = "Verizon", Category = StoreCategory.Electronics},
            new StoreNameCategory { Name = "TMobile", Category = StoreCategory.Electronics},
            new StoreNameCategory { Name = "Woolworth's", Category = StoreCategory.Grocery},
            new StoreNameCategory { Name = "K-Mart", Category = StoreCategory.Grocery},
            new StoreNameCategory { Name = "Bunning's", Category = StoreCategory.HomeImprovement},
            new StoreNameCategory { Name = "Telstra", Category = StoreCategory.Electronics},
            new StoreNameCategory { Name = "JB Hi-Fi", Category = StoreCategory.Electronics},
            new StoreNameCategory { Name = "Best Buy", Category = StoreCategory.Electronics},
            new StoreNameCategory { Name = "Fry's", Category = StoreCategory.Electronics},
            new StoreNameCategory { Name = "McDonald's", Category = StoreCategory.Restaurant}
        };

        private readonly IEnumerable<string> _zips = new List<string>
        {
            "77020", "77002", "77003", "77004", "77005", "77006 ", "77007", "77008", "77009", "77010", "77011",
            "77012", "77013", "77014", "77015", "77016", " 77017", "77018", "77019", "77020 ", "77021",
            "77022", "77023", "77024", "77025", "77026", "77027", "77028", "77029", "77030", "77031", "77032",
            "77033", "77034", "77035", "77036", "77037", "77038", "77039", "77040", "77041", "77042",
            "77043", "77044", "77045", "77046", "77047", "77048", "77049", "77050", "77051", "77053",
            "77054", "77055", "77056", "77057", "77058", "77059", "77060", "77061", "77062", "77063",
            "77064", "77065", "77066", "77067", "77068", "77069", "77070", "77071", "77072", "77073",
            "77074", "77075", "77076", "77077", "77078", "77079", "77080", "77081", "77082", "77083",
            "77084", "77085", "77086", "77087", "77088", "77089", "77090", "77091", "77092", "77093",
            "77094", "77095", "77096", "77098", "77099", "77201"
        };

        private IEnumerable<DateTimeOffset> _openingTimes = new List<DateTimeOffset>
        {
            new DateTimeOffset(2020, 01, 01, 07, 0,  0, TimeSpan.Zero),
            new DateTimeOffset(2020, 01, 01, 08, 0,  0, TimeSpan.Zero),
            new DateTimeOffset(2020, 01, 01, 09, 0,  0, TimeSpan.Zero)
        };

        private IEnumerable<DateTimeOffset> _closingTimes = new List<DateTimeOffset>
        {
            new DateTimeOffset(2020, 01, 01, 17, 0,  0, TimeSpan.Zero),
            new DateTimeOffset(2020, 01, 01, 18, 0,  0, TimeSpan.Zero),
            new DateTimeOffset(2020, 01, 01, 19, 0,  0, TimeSpan.Zero)
        };

        private readonly IEnumerable<TimeSpan> _timeSpans = new List<TimeSpan>
        {
            TimeSpan.FromMinutes(30),
            TimeSpan.FromMinutes(45),
            TimeSpan.FromMinutes(60),
            TimeSpan.FromMinutes(75),
            TimeSpan.FromMinutes(90)
        };

        public StoreDocumentGenerator()
        {
            var addressFaker =
                new Faker<Address>()
                    .RuleFor(x => x.AddressLine1, x => x.Address.StreetAddress())
                    .RuleFor(x => x.AddressLine2, x => x.Address.City())
                    .RuleFor(x => x.City, x => "Houston")
                    .RuleFor(x => x.State, x => "Texas")
                    .RuleFor(x => x.ZipCode, x => x.PickRandom(_zips));

            var coordinateFaker =
                new Faker<Coordinate>()
                    .RuleFor(x => x.Latitude, x => x.Address.Latitude(29.925131, 29.92901))
                    .RuleFor(x => x.Longitude, x => x.Address.Longitude(-95.552773, -95.207016));

            var faker =
                new Faker<StoreDocument>()
                    .RuleFor(x => x.Id, x => Guid.NewGuid())
                    .Rules((f, o) =>
                    {
                        var storeFaker = f.PickRandom(_stores);
                        o.Name = storeFaker.Name;
                        o.Category = storeFaker.Category;
                    })
                    .RuleFor(x => x.Address, x => addressFaker.Generate())
                    .RuleFor(x => x.Coordinate, x => coordinateFaker.Generate())
                    .RuleFor(x => x.AverageWait, x => x.PickRandom(_timeSpans))
                    .RuleFor(x => x.CurrentWait, x => x.PickRandom(_timeSpans))
                    .RuleFor(x => x.Email, x => x.Internet.Email())
                    .RuleFor(x => x.Phone, x => x.Phone.PhoneNumber());

            Items = faker.Generate(5000).ToList();
        }

        public List<StoreDocument> Items { get; set; }
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

    public class StoreNameCategory
    {
        public string Name { get; set; }

        public StoreCategory Category { get; set; }
    }
}