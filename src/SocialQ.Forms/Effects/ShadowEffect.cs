using Xamarin.Forms;

namespace SocialQ.Forms.Effects
{
    /// <summary>
    /// Shadow <see cref="RoutingEffect"/>. for a <see cref="Label"/>.
    /// </summary>
    public class ShadowEffect : RoutingEffect
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShadowEffect"/> class.
        /// </summary>
        public ShadowEffect()
            : base("SocialQ.LabelShadowEffect")
        {
        }

        /// <summary>
        /// Gets or sets the shadow radius.
        /// </summary>
        public float Radius { get; set; }

        /// <summary>
        /// Gets or sets the shadow color.
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// Gets or sets the shadow x distance.
        /// </summary>
        public float DistanceX { get; set; }

        /// <summary>
        /// Gets or sets the shadow y distance.
        /// </summary>
        public float DistanceY { get; set; }
    }
}