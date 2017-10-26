using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace Xamarin.Controls.CustomControls
{
    public class BindableRadioGroup : StackLayout
    {

        public List<CustomRadioButton> rads;

        public BindableRadioGroup()
        {

            rads = new List<CustomRadioButton>();
        }

        public static BindableProperty ItemsSourceProperty =
            BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(BindableRadioGroup), default(IEnumerable), propertyChanged: OnItemsSourceChanged);

        public static BindableProperty SelectedIndexProperty =
            BindableProperty.Create(nameof(SelectedIndex), typeof(int), typeof(BindableRadioGroup), default(int), BindingMode.TwoWay, propertyChanged: OnSelectedIndexChanged);

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }


        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }

        public event EventHandler<int> CheckedChanged;


        private static void OnItemsSourceChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var radButtons = bindable as BindableRadioGroup;

            radButtons.rads.Clear();
            radButtons.Children.Clear();

            var newEnumerable = newvalue as IEnumerable;

            if (newEnumerable == null) return;

            int radIndex = 0;
            foreach (var item in newEnumerable)
            {
                var rad = new CustomRadioButton();
                rad.Text = item.ToString();
                rad.ElementId = radIndex;

                rad.CheckedChanged += radButtons.OnCheckedChanged;

                radButtons.rads.Add(rad);

                radButtons.Children.Add(rad);
                radIndex++;
            }
        }

        private void OnCheckedChanged(object sender, EventArgs<bool> e)
        {

            if (e.Value == false) return;

            var selectedRad = sender as CustomRadioButton;

            foreach (var rad in rads)
            {
                if (!selectedRad.ElementId.Equals(rad.ElementId))
                {
                    rad.Checked = false;
                }
                else
                {
                    CheckedChanged?.Invoke(sender, rad.ElementId);
                }

            }

        }

        private static void OnSelectedIndexChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (oldValue is int && newValue is int)
                OnSelectedIndexChanged(bindable, (int)oldValue, (int)newValue);
        }

        private static void OnSelectedIndexChanged(BindableObject bindable, int oldvalue, int newvalue)
        {
            if (newvalue == -1) return;

            var bindableRadioGroup = bindable as BindableRadioGroup;


            foreach (var rad in bindableRadioGroup.rads)
            {
                if (rad.ElementId == bindableRadioGroup.SelectedIndex)
                {
                    rad.Checked = true;
                }

            }


        }

    }
}
