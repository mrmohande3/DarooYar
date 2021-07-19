using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DarooYar.Models
{
    public class BloodTest
    {
        [Key]
        public int BloodTestId { get; set; }
        public int BloodPressure { get; set; }
        public int BloodSugar { get; set; }
        public DateTime DateTime { get; set; }
        public TimeSpan TimeSpan { get; set; }
    }
}
