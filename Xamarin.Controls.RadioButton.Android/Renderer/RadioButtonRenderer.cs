using Android.Widget;
using Xamarin.Forms;
using Xamarin.Controls.CustomControls;
using Xamarin.Forms.Platform.Android;
using Xamarin.Controls.CustomControls.Android.Renderer;


[assembly: ExportRenderer(typeof(CustomRadioButton), typeof(RadioButtonRenderer))]
namespace Xamarin.Controls.CustomControls.Android.Renderer
{
    public class RadioButtonRenderer : ViewRenderer<CustomRadioButton, RadioButton>
    {
        bool isBusy = false;

        protected override void OnElementChanged(ElementChangedEventArgs<CustomRadioButton> e)
        {
            base.OnElementChanged(e);

            isBusy = true;

            try
            {
                if (e.OldElement != null)
                {
                    e.OldElement.PropertyChanged += ElementOnPropertyChanged;
                }

                if (this.Control == null)
                {
                    var radButton = new RadioButton(this.Context);
                    radButton.CheckedChange += radButton_CheckedChange;

                    this.SetNativeControl(radButton);
                }

                Control.Text = e.NewElement.Text;
                Control.Checked = e.NewElement.Checked;

                Element.PropertyChanged += ElementOnPropertyChanged;
            }
            finally
            {
                isBusy = false;
            }
        }

        void radButton_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if (isBusy) return;
            this.Element.Checked = e.IsChecked;
        }

        void ElementOnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (isBusy) return;
            if (Control == null || Element == null) return;

            switch (e.PropertyName)
            {
                case "Checked":
                    Control.Checked = Element.Checked;
                    break;
                case "Text":
                    Control.Text = Element.Text;
                    break;

            }
        }
    }
}