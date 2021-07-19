using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DarooYar.Views.ItemTemplate
{


    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DayItemTemplate : ContentView
    {


        #region IsSelected
        private bool _isSelected;
        public bool IsSelected
        {
            get { return (bool)this.GetValue(IsSelectedProperty); }
            set { this.SetValue(IsSelectedProperty, value); }
        }
        public static readonly BindableProperty IsSelectedProperty = BindableProperty.Create(
            nameof(IsSelected)
            , typeof(bool)
            , typeof(DayItemTemplate)
            , false
            , BindingMode.TwoWay
            , null
            , IsSelectedPropertyChanged);
        private static void IsSelectedPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (DayItemTemplate)bindable;
            var slected = (bool)newValue;
            control.SelectedChange(slected);
        }

        public void SelectedChange(bool sender)
        {
            _isSelected = sender;
            if (sender)
            {
                dateFrame.BackgroundColor = (Color) Application.Current.Resources["PrimaryColor"];
                dateLable.TextColor = Color.White;
                dayLable.TextColor = Color.Black;
            }
            else
            {
                dateFrame.BackgroundColor = Color.FromHex("#eee");
                dateLable.TextColor = Color.FromHex("#888");
                dayLable.TextColor = Color.FromHex("#888");
            }
        }
        #endregion

        public DayItemTemplate()
        {
            InitializeComponent();
        }
    }
}