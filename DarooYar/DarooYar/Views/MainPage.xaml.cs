
using System;
using System.Collections.Generic;
using DarooYar.Models.ItemModel;
using DarooYar.ViewModels;
using DarooYar.Views.ItemTemplate;

namespace DarooYar.Views
{
    public partial class MainPage
    {
        public List<DayItemModel> ReminderDayItemModels = new List<DayItemModel>();
        private DayItemTemplate selectedReminderDay = null;


        public List<DayItemModel> TestDayItemModels = new List<DayItemModel>();
        private DayItemTemplate selectedTestDay = null;
        public MainPage()
        {
            InitializeComponent();
            for (int i = 0; i < 30; i++)
            {
                var date = DateTime.Now.AddDays(i).Date;
                ReminderDayItemModels.Add(new DayItemModel{DateTime = date});
                TestDayItemModels.Add(new DayItemModel { DateTime = date });
            }

            reminderDayCollection.ItemsSource = ReminderDayItemModels;
            testDayCollection.ItemsSource = TestDayItemModels;
        }

        private void TestItemModel_Tppaed(object sender, EventArgs e)
        {
            if (sender is DayItemTemplate dayItemModel && BindingContext is MainPageViewModel viewModel)
            {
                if (selectedTestDay != null)
                    selectedTestDay.IsSelected = false;
                selectedTestDay = dayItemModel;
                selectedTestDay.IsSelected = true;
                viewModel.SelectedDayTestCommand.Execute(dayItemModel.BindingContext);
            }
        }

        private void ReminderItemModel_Tppaed(object sender, EventArgs e)
        {
            if (sender is DayItemTemplate dayItemModel && BindingContext is MainPageViewModel viewModel)
            {
                if (selectedReminderDay != null)
                    selectedReminderDay.SelectedChange(false);
                dayItemModel.SelectedChange(true);
                selectedReminderDay = dayItemModel;
                viewModel.SelectedDayReminderCommand.Execute(dayItemModel.BindingContext);
            }
        }
    }
}
