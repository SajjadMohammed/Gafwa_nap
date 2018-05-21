using Android.App;
using Android.OS;
using Android.Support.V7.App;
using V7ToolBar = Android.Support.V7.Widget.Toolbar;
using Android.Content.PM;
using Android.Widget;
using Android.Graphics;
using System;
using Android.Gms.Ads;
using Android.Net.Wifi;
using Android.Views;

namespace Gafwa
{
    [Activity(Label = "انواع القيلولة", Theme = "@style/myTheme",NoHistory =true, ScreenOrientation = ScreenOrientation.Portrait)]
    public class GhafwaActivity : AppCompatActivity
    {
        V7ToolBar ghafwaToolBar;
        Button m6Button, m20Button, m30Button, m45Button, m60Button, m90Button;
        TextView m6TextView, m20TextView, m30TextView, m45TextView, m60TextView, m90TextView;
        TextView[] textViews;
        Button[] buttons;
        Alerts alerts;
        GhafwaSettings ghafwaSettings;
        DateTime now;
        InterstitialAd interstitialAdView;
        WifiManager wifi;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your application here
            Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
            SetContentView(Resource.Layout.GhafwaListLayout);
            ghafwaToolBar = FindViewById<V7ToolBar>(Resource.Id.ghafwaToolbar);
            SetSupportActionBar(ghafwaToolBar);
            m60TextView = FindViewById<TextView>(Resource.Id.m60TextView);
            m6TextView = FindViewById<TextView>(Resource.Id.m6TextView);
            m20TextView = FindViewById<TextView>(Resource.Id.m20TextView);
            m30TextView = FindViewById<TextView>(Resource.Id.m30TextView);
            m45TextView = FindViewById<TextView>(Resource.Id.m45TextView);
            m90TextView = FindViewById<TextView>(Resource.Id.m90TextView);
            textViews = new TextView[] { m6TextView, m20TextView, m30TextView, m45TextView, m60TextView, m90TextView };
            foreach (var item in textViews)
            {
                TextFont(item);
            }
            //
            MobileAds.Initialize(ApplicationContext, GetString(Resource.String.appPub));
            interstitialAdView = new InterstitialAd(this)
            {
                AdUnitId = GetString(Resource.String.interId)
            };
            var interListener = new InterAdListener(interstitialAdView);

            wifi = GetSystemService(WifiService) as WifiManager;

            interstitialAdView.LoadAd(new AdRequest.Builder().Build());

            // Load the next InterstitialAd
            interListener.OnAdClosed();
            interstitialAdView.AdListener = interListener;
            
            //
            m6Button = FindViewById<Button>(Resource.Id.m6Ghafwa);
            m6Button.Click += M6Button_Click;
            m20Button = FindViewById<Button>(Resource.Id.m20Ghafwa);
            m20Button.Click += M20Button_Click;
            m30Button = FindViewById<Button>(Resource.Id.m30Ghafwa);
            m30Button.Click += M30Button_Click;
            m45Button = FindViewById<Button>(Resource.Id.m45Ghafwa);
            m45Button.Click += M45Button_Click;
            m60Button = FindViewById<Button>(Resource.Id.m60Ghafwa);
            m60Button.Click += M60Button_Click;
            m90Button = FindViewById<Button>(Resource.Id.m90Ghafwa);
            m90Button.Click += M90Button_Click;
            buttons = new Button[] { m6Button, m20Button, m30Button, m45Button, m60Button, m90Button };
            foreach (var item in buttons)
            {
                ButtonFont(item);
            }
            //
            alerts = new Alerts(this);
            ghafwaSettings = new GhafwaSettings(this);
        }

        void RunInterAd()
        {
            //Interstial AdView
            if (interstitialAdView.IsLoaded)
            {
                interstitialAdView.Show();
            }
            else
            {
                interstitialAdView.LoadAd(new AdRequest.Builder().Build());
            }
            //
        }

        private void M90Button_Click(object sender, EventArgs e)
        {
            RunInterAd();
            alerts.SetAlert("غفوة الـ90 دقيق", "سيرن الهاتف بعد 90 دقيقة من الآن");
            SetGhafwa(909090, 90 * 60000, "غفوة الـ90 دقيقة", "لقد انتهت مدة غفوة الـ90 دقيقة");
        }

        private void M60Button_Click(object sender, EventArgs e)
        {
            RunInterAd();
            alerts.SetAlert("غفوة الـ60 دقيق", "سيرن الهاتف بعد 60 دقيقة من الآن");
            SetGhafwa(606060, 60 * 60000, "غفوة الـ60 دقيقة", "لقد انتهت مدة غفوة الـ60 دقيقة");
        }

        private void M45Button_Click(object sender, EventArgs e)
        {
            RunInterAd();
            alerts.SetAlert("غفوة الـ45 دقيق", "سيرن الهاتف بعد 45 دقيقة من الآن");
            SetGhafwa(454545, 45 * 60000, "غفوة الـ45 دقيقة", "لقد انتهت مدة غفوة الـ45 دقيقة");
        }

        private void M30Button_Click(object sender, EventArgs e)
        {
            RunInterAd();
            alerts.SetAlert("غفوة الـ30 دقيق", "سيرن الهاتف بعد 30 دقيقة من الآن");
            SetGhafwa(303030, 30 * 60000, "غفوة الـ30 دقيقة", "لقد انتهت مدة غفوة الـ30 دقيقة");
        }

        private void M20Button_Click(object sender, EventArgs e)
        {
            RunInterAd();
            alerts.SetAlert("غفوة الـ20 دقيق", "سيرن الهاتف بعد 20 دقيقة من الآن");
            SetGhafwa(202020, 20 * 60000, "غفوة الـ20 دقيقة", "لقد انتهت مدة غفوة الـ20 دقيقة");
        }

        private void M6Button_Click(object sender, EventArgs e)
        {
            RunInterAd();
            alerts.SetAlert("غفوة الـ6 دقائق", "سيرن الهاتف بعد 6 دقائق من الآن");
            SetGhafwa(666, 6 * 60000, "غفوة الست دقائق", "لقد انتهت مدة غفوة الست دقائق");
        }

        void SetGhafwa(int nId, long time, string title, string content)
        {
            now = DateTime.Now;

            Inputs.NotificationId = nId;
            Inputs.Title = title;
            Inputs.Content = content;
            
            ghafwaSettings.StartAlarmBroadcastReceiver(Java.Lang.JavaSystem.CurrentTimeMillis(), time, false, Inputs.NotificationId, Inputs.Title, Inputs.Content);
        }

        void TextFont(TextView textView) => textView.Typeface = Typeface.CreateFromAsset(base.Assets, "JF-Flat-regular.otf");
        void ButtonFont(Button button) => button.Typeface = Typeface.CreateFromAsset(base.Assets, "JF-Flat-regular.otf");
    }
}