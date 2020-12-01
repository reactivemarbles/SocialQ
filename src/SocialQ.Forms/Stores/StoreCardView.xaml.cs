using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace SocialQ.Forms
{
    /// <summary>
    /// Represents the store card view.
    /// </summary>
    public partial class StoreCardView
    {
        /// <summary>
        /// Gets the store name property.
        /// </summary>
        public static readonly BindableProperty StoreNameProperty =
            BindableProperty.Create(nameof(StoreName), typeof(string), typeof(StoreCardView));

        /// <summary>
        /// Gets the store color property.
        /// </summary>
        public static readonly BindableProperty StoreColorProperty =
            BindableProperty.Create(nameof(StoreColor), typeof(Color), typeof(StoreCardView));

        /// <summary>
        /// Gets the store current time property.
        /// </summary>
        public static readonly BindableProperty CurrentTimeProperty =
            BindableProperty.Create(nameof(CurrentTime), typeof(TimeSpan), typeof(StoreCardView));

        /// <summary>
        /// Gets the store close time property.
        /// </summary>
        public static readonly BindableProperty CloseTimeProperty =
            BindableProperty.Create(nameof(CloseTime), typeof(TimeSpan), typeof(StoreCardView));

        /// <summary>
        /// Gets the store add command.
        /// </summary>
        public static readonly BindableProperty AddCommandProperty =
            BindableProperty.Create(nameof(AddCommand), typeof(ICommand), typeof(StoreCardView));

        /// <summary>
        /// Initializes a new instance of the <see cref="StoreCardView"/> class.
        /// </summary>
        public StoreCardView() => InitializeComponent();

        /// <summary>
        /// Gets or sets the store name.
        /// </summary>
        public string StoreName
        {
            get => (string)GetValue(StoreNameProperty);
            set => SetValue(StoreNameProperty, value);
        }

        /// <summary>
        /// Gets or sets the store color.
        /// </summary>
        public Color StoreColor
        {
            get => (Color)GetValue(StoreColorProperty);
            set => SetValue(StoreColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the store current time.
        /// </summary>
        public TimeSpan CurrentTime
        {
            get => (TimeSpan)GetValue(CurrentTimeProperty);
            set => SetValue(CurrentTimeProperty, value);
        }

        /// <summary>
        /// Gets or sets the store close time.
        /// </summary>
        public TimeSpan CloseTime
        {
            get => (TimeSpan)GetValue(CloseTimeProperty);
            set => SetValue(CloseTimeProperty, value);
        }

        /// <summary>
        /// Gets or sets the store add command.
        /// </summary>
        public ICommand AddCommand
        {
            get => (ICommand)GetValue(AddCommandProperty);
            set => SetValue(AddCommandProperty, value);
        }
    }
}