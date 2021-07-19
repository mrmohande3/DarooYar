using System;
using System.Collections.Generic;
using System.Text;

namespace DarooYar.Dependencies
{
    public interface IAlarmDependency
    {
        public void SetAlarm(DateTime dateTime, TimeSpan timeSpan, string detail);
    }
}
