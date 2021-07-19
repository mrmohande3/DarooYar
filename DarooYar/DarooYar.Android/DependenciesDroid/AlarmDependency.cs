using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Provider;
using DarooYar.Dependencies;
using DarooYar.Droid.DependenciesDroid;
using DarooYar.Droid.Receiver;
using Java.Util;
using Plugin.CurrentActivity;
using Xamarin.Forms;
using Application = Android.App.Application;
using Calendar = Android.Icu.Util.Calendar;
using CalendarField = Android.Icu.Util.CalendarField;

[assembly: Dependency(typeof(AlarmDependency))]
namespace DarooYar.Droid.DependenciesDroid
{
    public class AlarmDependency : IAlarmDependency
    {
        public void SetAlarm(DateTime dateTime, TimeSpan timeSpan, string detail)
        {
            Calendar calendar = Calendar.GetInstance(Locale.Root);
            calendar.Set(CalendarField.HourOfDay,timeSpan.Hours);
            calendar.Set(CalendarField.Minute, timeSpan.Minutes);
            calendar.Set(CalendarField.Second, 0);
            calendar.Set(CalendarField.Millisecond, 0);
            long sdl = calendar.TimeInMillis;
            
            var current = CrossCurrentActivity.Current;

            var alarmIntent = new Intent(current.AppContext, typeof(AlarmReceiver));
            alarmIntent.PutExtra("title", "Hello");
            alarmIntent.PutExtra("message", "World!");

            var pending = PendingIntent.GetBroadcast(current.AppContext, 0, alarmIntent, PendingIntentFlags.UpdateCurrent);

            var alarmManager = current.Activity.GetSystemService(MainActivity.AlarmService).JavaCast<AlarmManager>();
            alarmManager.Set(AlarmType.RtcWakeup, SystemClock.ElapsedRealtime() + 5 * 1000, pending);


            var def = dateTime.Date - DateTime.Today.Date;
            Intent alarmIn = new Intent(AlarmClock.ActionSetAlarm);
            alarmIn.PutExtra(AlarmClock.ExtraHour, timeSpan.Hours);
            alarmIn.PutExtra(AlarmClock.ExtraMinutes, timeSpan.Minutes);
            if(def.Days>0)
                alarmIn.PutExtra(AlarmClock.ExtraDays, def.Days);
            alarmIn.PutExtra(AlarmClock.ExtraMessage, detail);
            current.Activity.StartActivity(alarmIn);
        }
    }
}