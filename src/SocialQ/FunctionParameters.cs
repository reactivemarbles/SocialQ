namespace SocialQ
{
    /// <summary>
    /// Parameters for functions.
    /// </summary>
    public class FunctionParameters
    {
        /// <summary>
        /// Gets the default parameter.
        /// </summary>
        public static FunctionParameters Default => new FunctionParameters();

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        public string? Code { get; set; }
    }
}