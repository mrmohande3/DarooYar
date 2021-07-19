using System;
using System.Collections.Generic;
using System.Text;
using MD.PersianDateTime.Standard;

namespace DarooYar.Models.ItemModel
{
    public class DayItemModel
    {
        public DateTime DateTime { get; set; } = DateTime.Now;

        public string DayName
        {
            get
            {
                return (new PersianDateTime(DateTime).GetLongDayOfWeekName);
            }
        }

        public string MonthDate 
        {
            get
            {
                return (new PersianDateTime(DateTime).ToString("MMMM dd"));
            }
        }
    }
}
