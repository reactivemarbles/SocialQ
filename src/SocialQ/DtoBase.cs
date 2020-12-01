using System;

namespace SocialQ
{
    /// <summary>
    /// base data transfer object.
    /// </summary>
    public abstract class DtoBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DtoBase"/> class.
        /// </summary>
        protected DtoBase() => Id = Guid.NewGuid();

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public Guid Id { get; set; }
    }
}