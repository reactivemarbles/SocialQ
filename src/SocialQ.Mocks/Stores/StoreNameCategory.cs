using SocialQ.Stores;

namespace SocialQ.Mocks.Stores
{
    /// <summary>
    /// Represents a store name category.
    /// </summary>
    public class StoreNameCategory
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        public StoreCategory Category { get; set; }
    }
}