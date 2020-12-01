using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using Foundation;
using UIKit;

namespace SocialQ.iOS
{
    /// <summary>
    /// The iOS Application.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1649:FileNameMustMatchTypeName", Justification = "iOS is fun.")]
    public class Application
    {
        /// <summary>
        /// The application extension point.
        /// </summary>
        /// <param name="args">The arguments.</param>
        private static void Main(string[] args) =>
            UIApplication.Main(args, null, "AppDelegate");
    }
}
