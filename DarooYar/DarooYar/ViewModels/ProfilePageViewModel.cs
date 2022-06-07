using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using DarooYar.Models;
using DarooYar.Utilities;
using Microsoft.EntityFrameworkCore;
using Prism.Navigation;
using Rg.Plugins.Popup.Services;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;

namespace DarooYar.ViewModels
{
    public class ProfilePageViewModel : ViewModelBase
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public int Age { get; set; }
        public ICommand EditUserCommand { get; set; }
        private List<BloodTest> _thisMonthTests = new List<BloodTest>();
        private List<Reminder> _thisMonthReminders = new List<Reminder>();

        public int BloodPressureAvg { get; set; }
        public int BloodSugarAvg { get; set; }
        public int Reminders { get; set; }
        public ProfilePageViewModel(INavigationService navigationService) : base(navigationService)
        {

        }
        
        public override async void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            FullName = Preferences.Get("FullName", string.Empty);
            PhoneNumber = Preferences.Get("PhoneNumber", string.Empty);
            Age = Preferences.Get("Age", 0);
            _thisMonthReminders = await RepositoryWrapper.GetBaseRepository<Reminder>().TableNoTracking
                .Where(r => r.DateTime.Date >= DateTime.Today.AddMonths(-1)).ToListAsync();
            _thisMonthTests = await RepositoryWrapper.GetBaseRepository<BloodTest>().TableNoTracking
                .Where(r => r.DateTime.Date >= DateTime.Today.AddMonths(-1)).ToListAsync();
            Reminders = _thisMonthReminders.Count;
            if (_thisMonthTests.Count > 0)
                BloodSugarAvg = _thisMonthTests.Sum(t => t.BloodSugar) / _thisMonthTests.Count;
            if (_thisMonthTests.Count > 0)
                BloodPressureAvg = _thisMonthTests.Sum(t => t.BloodPressure) / _thisMonthTests.Count;


        }

        public override void InitializeCommand()
        {
            base.InitializeCommand();
            EditUserCommand = new DelegateCommand(() =>
            {
                Xamarin.Essentials.Preferences.Set("FullName", FullName);
                Xamarin.Essentials.Preferences.Set("PhoneNumber", PhoneNumber);
                Xamarin.Essentials.Preferences.Set("Age", Age);
            });
        }
    }
}
