using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DarooYar.Models
{
    public class Reminder
    {
        [Key]
        public int ReminderId { get; set; }
        public string MedicineName { get; set; }
        public string MedicineType { get; set; }
        public int Value { get; set; }
        public string Detail { get; set; }
        public DateTime DateTime { get; set; }
        public TimeSpan TimeSpan { get; set; }
    }
}
