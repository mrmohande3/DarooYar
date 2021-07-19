using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DarooYar.Models;
using DarooYar.Utilities;
using MD.PersianDateTime.Standard;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DarooYar.Views.PopUps
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddTestPopUp : ContentView
    {
        private DateTime selecteDateTime;
        public event EventHandler Added;
        public AddTestPopUp(DateTime dateTime)
        {
            InitializeComponent();
            selecteDateTime = dateTime;
            dateLabel.Text = (new PersianDateTime(selecteDateTime)).ToString("dddd , dd MMMM");
        }

        private void CloseButton_OnClicked(object sender, EventArgs e)
        {
            PopUpUtilities.Instance.PopAsync();
        }

        private async void  SubmiButton_OnClicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(bloodPEntry.Text))
                    throw new Exception("نام دارو را وارد کنید");
                if (string.IsNullOrEmpty(bloodSEntry.Text))
                    throw new Exception("تعداد دارو را وارد کنید");
                var test = new BloodTest()
                {
                    DateTime = selecteDateTime,
                    TimeSpan = timePicker.Time,
                    BloodSugar = int.Parse(bloodPEntry.Text),
                    BloodPressure = int.Parse(bloodPEntry.Text)
                };
                await RepositoryWrapper.GetBaseRepository<BloodTest>().AddAsync(test);
                Added?.Invoke(test, new EventArgs());
            }
            catch (Exception exception)
            {
                PopUpUtilities.Instance.PushError(exception.Message);
            }
        }
    }
}