using System;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Util;
using Prism;
using Prism.Ioc;
using Rg.Plugins.Popup;
using Sharpnado.MaterialFrame;
using Xamarin.Forms;

namespace DarooYar.Droid
{
    [Activity(Theme = "@style/MainTheme",
              ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            try
            {
                TabLayoutResource = Resource.Layout.Tabbar;
                ToolbarResource = Resource.Layout.Toolbar;

                base.OnCreate(savedInstanceState);
                Popup.Init(this);
            
                Initializer.Initialize(false,true);
                Forms.Init(this, savedInstanceState);
                LoadApplication(new App(new AndroidInitializer()));
            }
            catch (Exception e)
            {
                Log.Error("DaroYarError", e.Message);
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
        }
    }
}

