namespace SocialQ.Stores
{
    /// <summary>
    /// Represents an address of a physical location.
    /// </summary>
    public class Address
    {
        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        public string? AddressLine1 { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        public string? AddressLine2 { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        public string? City { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        public string? State { get; set; }

        /// <summary>
        /// Gets or sets the zip code.
        /// </summary>
        public string? ZipCode { get; set; }
    }
}