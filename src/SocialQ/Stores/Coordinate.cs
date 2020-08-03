namespace SocialQ
{
    public class Coordinate
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public static Coordinate Default => new Coordinate { Latitude = 0, Longitude = 0 };
    }
}