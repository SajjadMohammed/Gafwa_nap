using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using V7ToolBar = Android.Support.V7.Widget.Toolbar;
using Android.Content.PM;
using Android.Content;
using Android.Views;
using Android.Gms.Ads;
using Android.Net.Wifi;
using Java.Util;
using System;

namespace Gafwa
{
    [Activity(Label = "غفوة",MainLauncher =true, Theme = "@style/myTheme", ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : AppCompatActivity
    {
        V7ToolBar mainToolBar;
        Button ghafwaButton;
        bool isStarting = false;
        GhafwaSettings ghafwaSettings;
        ISharedPreferences sharedPreferences;
        ISharedPreferencesEditor sharedPreferencesEditor;
        RateClass rateClass;

        AdView adView;
        InterstitialAd interstitialAdView;
        WifiManager wifi;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
            SetContentView(Resource.Layout.Main);
            mainToolBar = FindViewById<V7ToolBar>(Resource.Id.mainToolbar);
            //Toolbar will now take on default actionbar characteristics
            SetSupportActionBar(mainToolBar);
            SupportActionBar.Title = "غفوة";
            ghafwaButton = FindViewById<Button>(Resource.Id.startGhafwa);
            ghafwaSettings = new GhafwaSettings(this);
            sharedPreferences = GetSharedPreferences("start", FileCreationMode.Private);
            isStarting = sharedPreferences.GetBoolean("starting", false);
            if (!isStarting)
            {
                ghafwaButton.Text = "تشغيل";
            }
            else
            {
                ghafwaButton.Text = "ايقاف";
            }
            ghafwaButton.Click += GhafwaButton_Click;

            adView = FindViewById<AdView>(Resource.Id.AdView);
            MobileAds.Initialize(ApplicationContext, GetString(Resource.String.appPub));
            interstitialAdView = new InterstitialAd(this)
            {
                AdUnitId = GetString(Resource.String.interId)
            };
            var interListener = new InterAdListener(interstitialAdView);

            wifi = GetSystemService(WifiService) as WifiManager;
            if (!wifi.IsWifiEnabled)
                adView.Visibility = ViewStates.Gone;
            else
            {
                adView.LoadAd(new AdRequest.Builder().Build());
                interstitialAdView.LoadAd(new AdRequest.Builder().Build());

                // Load the next InterstitialAd
                interListener.OnAdClosed();
                interstitialAdView.AdListener = interListener;
            }
            //
            rateClass = new RateClass();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.aboutMenu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.rate:
                    rateClass.RateApp();
                    return true;
                case Resource.Id.dollar:
                    if (wifi.IsWifiEnabled)
                    {
                        adView.Visibility = ViewStates.Visible;
                    }
                    //Interstial AdView
                    if (interstitialAdView.IsLoaded)
                    {
                        interstitialAdView.Show();
                    }
                    else
                    {
                        interstitialAdView.LoadAd(new AdRequest.Builder().Build());
                    }
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        private void GhafwaButton_Click(object sender, System.EventArgs e)
        {
            if (wifi.IsWifiEnabled)
            {
                adView.Visibility = ViewStates.Visible;
            }
            //Interstial AdView
            if (interstitialAdView.IsLoaded)
            {
                interstitialAdView.Show();
            }
            else
            {
                interstitialAdView.LoadAd(new AdRequest.Builder().Build());
            }

            if (ghafwaButton.Text.Equals("تشغيل"))
            {
                var alertMngr = new Android.Support.V7.App.AlertDialog.Builder(this);
                alertMngr.SetTitle("غفوة");
                alertMngr.SetMessage("سيرن هاتفك كل يوم في الساعة الثانية من بعد الظهر لتختار نوع ومدة الغفوة");
                alertMngr.SetPositiveButton("موافق",
                    delegate
                    {
                        Inputs.NotificationId = 111;
                        Inputs.Title = "بدء الغفوة";
                        Inputs.Content = "حان الوقت لبدء غفوتك، انقر هنا لاختيار نوع مدة الغفوة";

                        Calendar calNow = Calendar.Instance;
                        var calSet = (Calendar)calNow.Clone();
                        calSet.Set(CalendarField.HourOfDay, 14);
                        calSet.Set(CalendarField.Minute, 00);
                        calSet.Set(CalendarField.Second, 0);
                        calSet.Set(CalendarField.Millisecond, 0);

                        if (calSet.CompareTo(calNow) <= 0)
                        {
                            //Today Set time passed, count to tomorrow
                            calSet.Add(CalendarField.Date, 1);
                        }

                        ghafwaSettings.StartAlarmBroadcastReceiver(calSet.TimeInMillis, 0, true, Inputs.NotificationId, Inputs.Title, Inputs.Content);

                        sharedPreferencesEditor = GetSharedPreferences("start", FileCreationMode.Private).Edit();
                        sharedPreferencesEditor.PutBoolean("starting", true);
                        sharedPreferencesEditor.Apply();
                        ghafwaButton.Text = "ايقاف";
                    });
                alertMngr.SetNegativeButton("الغاء", delegate { });
                alertMngr.Show();
            }
            else
            {
                ghafwaSettings.StopAlarm(111);
                sharedPreferencesEditor = GetSharedPreferences("start", FileCreationMode.Private).Edit();
                sharedPreferencesEditor.PutBoolean("starting", false);
                sharedPreferencesEditor.Apply();
                ghafwaButton.Text = "تشغيل";
                Toast.MakeText(this, "تم ايقاف عمل الغفوة، سوف لن يتم تذكيرك كل يوم بعد الآن", ToastLength.Long).Show();
            }
        }
    }
}