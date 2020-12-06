using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using CoreGraphics;
using SocialQ.Forms.Effects;
using SocialQ.iOS.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("SocialQ")]
[assembly: ExportEffect(typeof(LabelShadowEffect), "LabelShadowEffect")]

[assembly: SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "iOS is a thing.")]

namespace SocialQ.iOS.Effects
{
    /// <summary>
    /// Represents the ios shadow effect.
    /// </summary>
    public class LabelShadowEffect : PlatformEffect
    {
        /// <inheritdoc/>
        protected override void OnAttached()
        {
            try
            {
                var effect = (ShadowEffect)Element.Effects.FirstOrDefault(e => e is ShadowEffect);
                if (effect != null)
                {
                    Control.Layer.ShadowRadius = effect.Radius;
                    Control.Layer.ShadowColor = effect.Color.ToCGColor();
                    Control.Layer.ShadowOffset = new CGSize(effect.DistanceX, effect.DistanceY);
                    Control.Layer.ShadowOpacity = 1.0f;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot set property on attached control. Error: {0}", ex.Message);
            }
        }

        /// <inheritdoc/>
        protected override void OnDetached()
        {
        }
    }
}