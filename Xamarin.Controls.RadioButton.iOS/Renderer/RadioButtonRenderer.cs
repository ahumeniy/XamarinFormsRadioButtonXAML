using System;
using System.ComponentModel;
using Xamarin.Forms;

using Xamarin.Forms.Platform.iOS;

using Xamarin.Controls.CustomControls;
using Xamarin.Controls.CustomControls.iOS.Controls;
using UIKit;
using CoreGraphics;

[assembly: ExportRenderer(typeof(CustomRadioButton), typeof(RadioButtonRenderer))]

namespace Xamarin.Controls.CustomControls.iOS.Controls
{
    /// <summary>
    /// The Radio button renderer for iOS.
    /// </summary>
    public class RadioButtonRenderer : ViewRenderer<CustomRadioButton, RadioButtonView>
    {
        bool isBusy = false;

        public RadioButtonRenderer()
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<CustomRadioButton> e)
        {
            base.OnElementChanged(e);

            System.Diagnostics.Debug.WriteLine("Renderizando Radio Button...");

            BackgroundColor = Element.BackgroundColor.ToUIColor();

            if (Control == null)
            {
                var checkBox = new RadioButtonView(Bounds);
                checkBox.TouchUpInside += (s, args) => Element.Checked = Control.Checked;

                SetNativeControl(checkBox);
            }

            isBusy = true;
            try
            {
                Control.LineBreakMode = UILineBreakMode.CharacterWrap;
                Control.VerticalAlignment = UIControlContentVerticalAlignment.Top;
                Control.Text = e.NewElement.Text;
                Control.Checked = e.NewElement.Checked;
                Control.SetTitleColor(e.NewElement.TextColor.ToUIColor(), UIControlState.Normal);
                Control.SetTitleColor(e.NewElement.TextColor.ToUIColor(), UIControlState.Selected);
            }
            finally
            {
                isBusy = false;
            }
        }

        private void ResizeText()
        {
            var text = this.Element.Text;


            var bounds = this.Control.Bounds;

            var width = this.Control.TitleLabel.Bounds.Width;

            var height = text.StringHeight(this.Control.Font, (float)width);

            var minHeight = string.Empty.StringHeight(this.Control.Font, (float)width);

            var requiredLines = Math.Round(height / minHeight, MidpointRounding.AwayFromZero);

            var supportedLines = Math.Round(bounds.Height / minHeight, MidpointRounding.ToEven);

            if (supportedLines != requiredLines)
            {
                bounds.Height += (float)(minHeight * (requiredLines - supportedLines));
                this.Control.Bounds = bounds;
                this.Element.HeightRequest = bounds.Height;
            }
        }

        public override void Draw(CGRect rect)
        {
            base.Draw(rect);
            ResizeText();
        }



        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (isBusy) return;

            switch (e.PropertyName)
            {
                case "Checked":
                    Control.Checked = Element.Checked;
                    break;
                case "Text":
                    Control.Text = Element.Text;
                    break;
                case "TextColor":
                    Control.SetTitleColor(Element.TextColor.ToUIColor(), UIControlState.Normal);
                    Control.SetTitleColor(Element.TextColor.ToUIColor(), UIControlState.Selected);
                    break;
                case "Element":
                    break;
                default:
                    System.Diagnostics.Debug.WriteLine("Property change for {0} has not been implemented.", e.PropertyName);
                    return;
            }
        }
    }
}