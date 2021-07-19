using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DarooYar.Dependencies;
using DarooYar.Models;
using DarooYar.Utilities;
using MD.PersianDateTime.Standard;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DarooYar.Views.PopUps
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddReminderPopUp : ContentView
    {
        public event EventHandler Added;
        private DateTime selecteDateTime;
        public AddReminderPopUp(DateTime dateTime)
        {
            InitializeComponent();
            selecteDateTime = dateTime;
            dateLabel.Text = (new PersianDateTime(selecteDateTime)).ToString("dddd , dd MMMM");
        }

        private async void SubmitButton_OnClicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(nameEntry.Text))
                    throw new Exception("نام دارو را وارد کنید");
                if (string.IsNullOrEmpty(countEntry.Text))
                    throw new Exception("تعداد دارو را وارد کنید");
                if (string.IsNullOrEmpty(detailEntry.Text))
                    throw new Exception("دلیل مصرف دارو را وارد کنید");
                if (string.IsNullOrEmpty(typeEntry.Text))
                    throw new Exception("نوع دارو را وارد کنید");
                var medicine = new Reminder
                {
                    DateTime = selecteDateTime,
                    TimeSpan = timePicker.Time,
                    Detail = detailEntry.Text,
                    MedicineName = nameEntry.Text,
                    MedicineType = typeEntry.Text,
                    Value = int.Parse(countEntry.Text)
                };
                DependencyService.Get<IAlarmDependency>().SetAlarm(selecteDateTime,timePicker.Time,medicine.Detail);
                await RepositoryWrapper.GetBaseRepository<Reminder>().AddAsync(medicine);
                Added?.Invoke(medicine, new EventArgs());
            }
            catch (Exception exception)
            {
                PopUpUtilities.Instance.PushError(exception.Message);
            }
        }

        private void CloseButton_OnClicked(object sender, EventArgs e)
        {
            PopUpUtilities.Instance.PopAsync();
        }
    }
}