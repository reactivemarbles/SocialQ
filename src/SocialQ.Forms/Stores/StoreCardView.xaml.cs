using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace SocialQ.Forms
{
    public partial class StoreCardView
    {
        public static readonly BindableProperty StoreNameProperty =
            BindableProperty.Create(nameof(StoreName), typeof(string), typeof(StoreCardView));

        public static readonly BindableProperty StoreColorProperty =
            BindableProperty.Create(nameof(StoreColor), typeof(Color), typeof(StoreCardView));

        public static readonly BindableProperty CurrentTimeProperty =
            BindableProperty.Create(nameof(CurrentTime), typeof(TimeSpan), typeof(StoreCardView));

        public static readonly BindableProperty CloseTimeProperty =
            BindableProperty.Create(nameof(CloseTime), typeof(TimeSpan), typeof(StoreCardView));

        public static readonly BindableProperty AddCommandProperty =
            BindableProperty.Create(nameof(AddCommand), typeof(ICommand), typeof(StoreCardView));

        public string StoreName
        {
            get => (string) GetValue(StoreNameProperty);
            set => SetValue(StoreNameProperty, value);
        }

        public Color StoreColor
        {
            get => (Color) GetValue(StoreColorProperty);
            set => SetValue(StoreColorProperty, value);
        }

        public TimeSpan CurrentTime
        {
            get => (TimeSpan) GetValue(CurrentTimeProperty);
            set => SetValue(CurrentTimeProperty, value);
        }

        public TimeSpan CloseTime
        {
            get => (TimeSpan) GetValue(CloseTimeProperty);
            set => SetValue(CloseTimeProperty, value);
        }

        public ICommand AddCommand
        {
            get => (ICommand) GetValue(AddCommandProperty);
            set => SetValue(AddCommandProperty, value);
        }

        public StoreCardView()
        {
            InitializeComponent();
        }
    }
}