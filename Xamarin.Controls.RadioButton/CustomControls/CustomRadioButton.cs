using System;
using Xamarin.Forms;

namespace Xamarin.Controls.CustomControls
{
    public class CustomRadioButton : Label
    {
        public static readonly BindableProperty CheckedProperty =
            BindableProperty.Create(nameof(Checked), typeof(bool), typeof(CustomRadioButton), false);

        /// <summary>
        /// The checked changed event.
        /// </summary>
        public event EventHandler<EventArgs<bool>> CheckedChanged;

        public static readonly BindableProperty ElementIdProperty =
            BindableProperty.Create(nameof(ElementId), typeof(int), typeof(CustomRadioButton), 0);

        /// <summary>
        /// Gets or sets a value indicating whether the control is checked.
        /// </summary>
        /// <value>The checked state.</value>
        public bool Checked
        {
            get => (bool)GetValue(CheckedProperty);
            set
            {
                SetValue(CheckedProperty, value);
                CheckedChanged?.Invoke(this, value);
            }
        }

        public int ElementId
        {
            get => (int)GetValue(ElementIdProperty);
            set => SetValue(ElementIdProperty, value);
        }
    }


}
