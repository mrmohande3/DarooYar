using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DarooYar.Models;
using DarooYar.Models.ItemModel;
using DarooYar.Utilities;
using DarooYar.Views.PopUps;
using Xamarin.Forms.Internals;

namespace DarooYar.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public ICommand AddReminderCommand { get; set; }
        public ICommand AddTestCommand { get; set; }
        public ICommand ProfilePageCommand { get; set; }


        public ICommand SelectedDayTestCommand { get; set; }
        public ICommand SelectedDayReminderCommand { get; set; }

        private List<BloodTest> _bloodTests = new List<BloodTest>();
        private List<Reminder> _reminders = new List<Reminder>();
        public DateTime SelectedDateTimeReminder { get; set; } = DateTime.Now;
        public DateTime SelectedDateTimeTest { get; set; } = DateTime.Now;
        public ObservableCollection<BloodTest> BloodTests { get; set; } = new ObservableCollection<BloodTest>();
        public ObservableCollection<Reminder> Reminders { get; set; } = new ObservableCollection<Reminder>();

        public DayItemModel SelectedDayTest { get; set; } = new DayItemModel {DateTime = DateTime.Today};
        public DayItemModel SelectedDayReminder { get; set; } = new DayItemModel { DateTime = DateTime.Today };
        public MainPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Main Page";
        }

        public override async void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);

            await Init();
        }

        private async Task Init()
        {
            BloodTests.Clear();
            Reminders.Clear();

            _bloodTests = await RepositoryWrapper.GetBaseRepository<BloodTest>().GetAllAsync();
            _reminders = await RepositoryWrapper.GetBaseRepository<Reminder>().GetAllAsync();

            _bloodTests.Where(b=>b.DateTime.Date==SelectedDateTimeTest.Date)
                .ForEach(f=>BloodTests.Add(f));
            _reminders.Where(b => b.DateTime.Date == SelectedDateTimeReminder.Date)
                .ForEach(f => Reminders.Add(f));
        }

        public override void InitializeCommand()
        {
            base.InitializeCommand();
            AddReminderCommand = new DelegateCommand(async () =>
            {
                var dialog = new AddReminderPopUp(SelectedDateTimeReminder);
                dialog.Added += (sender, args) =>
                {
                    Init();
                };
                await PopUpUtilities.Instance.PushAsync(dialog);
            });
            AddTestCommand = new DelegateCommand(async () =>
            {
                var dialog = new AddTestPopUp(SelectedDateTimeTest);
                dialog.Added += (sender, args) =>
                {
                    Init();
                };
                await PopUpUtilities.Instance.PushAsync(dialog);
            });
            ProfilePageCommand = new DelegateCommand(async () =>
            {
                await NavigationService.NavigateAsync("ProfilePage");
            });
            SelectedDayReminderCommand = new DelegateCommand<DayItemModel>(model =>
            {
                Reminders.Clear();
                SelectedDateTimeReminder = model.DateTime;
                _reminders.Where(b => b.DateTime.Date == SelectedDateTimeReminder.Date)
                    .ForEach(f => Reminders.Add(f));
            });
            SelectedDayTestCommand = new DelegateCommand<DayItemModel>(model =>
            {
                BloodTests.Clear();
                SelectedDateTimeTest = model.DateTime;
                _bloodTests.Where(b => b.DateTime.Date == SelectedDateTimeTest.Date)
                    .ForEach(f => BloodTests.Add(f));
            });
        }
    }
}
