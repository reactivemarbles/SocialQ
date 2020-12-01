namespace SocialQ.Stores
{
    /// <summary>
    /// Represents a geographic coordinate.
    /// </summary>
    public class Coordinate
    {
        /// <summary>
        /// Gets a default coordinate.
        /// </summary>
        public static Coordinate Default => new Coordinate { Latitude = 0, Longitude = 0 };

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        public double Longitude { get; set; }
    }
}