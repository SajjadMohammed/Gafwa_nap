using Android.App;
using Android.OS;
using Android.Support.V7.App;
using V7ToolBar = Android.Support.V7.Widget.Toolbar;
using Android.Content.PM;
using Android.Widget;
using Android.Views;
using System.Collections.Generic;

namespace Gafwa
{
    [Activity(Label = "انواع القيلولة", Theme = "@style/myTheme",NoHistory =true, ScreenOrientation = ScreenOrientation.Portrait)]
    public class GhafwaActivity : AppCompatActivity
    {
        V7ToolBar ghafwaToolBar;        
        ListView ghafwaListView;
        GhafwaAdapter ghafwaAdapter;
        List<GhafwaDiscreption> ghafwaDiscreptions;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your application here
            Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
            SetContentView(Resource.Layout.GhafwaListLayout);

            ghafwaToolBar = FindViewById<V7ToolBar>(Resource.Id.ghafwaToolbar);
            SetSupportActionBar(ghafwaToolBar);

            ghafwaListView = FindViewById<ListView>(Resource.Id.ghafwaListView);
            ghafwaDiscreptions = new List<GhafwaDiscreption>
            {
                new GhafwaDiscreption
                {
                    NotificationId=666,
                    GhafwaTime=6 * 60000,
                    GhafwaString="غفوة الـ6 دقائق",
                    GhafwaContent="سيرن الهاتف بعد 6 دقائق من الآن",
                    EndNotificationString="لقد انتهت مدة غفوة الست دقائق",
                    GhafwaDiscribe=GetString(Resource.String.m6)
                },
               new GhafwaDiscreption
                {
                    NotificationId=202020,
                    GhafwaTime=20 * 60000,
                    GhafwaString="غفوة الـ20 دقيق",
                    GhafwaContent="سيرن الهاتف بعد 20 دقيقة من الآن",
                    EndNotificationString="لقد انتهت مدة غفوة ال20 دقيقة",
                    GhafwaDiscribe=GetString(Resource.String.m20)
                },
               new GhafwaDiscreption
                {
                    NotificationId=303030,
                    GhafwaTime=30 * 60000,
                    GhafwaString="غفوة الـ30 دقيق",
                    GhafwaContent="سيرن الهاتف بعد 30 دقيقة من الآن",
                    EndNotificationString="لقد انتهت مدة غفوة ال30 دقيقة",
                    GhafwaDiscribe=GetString(Resource.String.m30)
                },
               new GhafwaDiscreption
                {
                    NotificationId=454545,
                    GhafwaTime=45 * 60000,
                    GhafwaString="غفوة الـ45 دقيق",
                    GhafwaContent="سيرن الهاتف بعد 45 دقيقة من الآن",
                    EndNotificationString="لقد انتهت مدة غفوة ال45 دقيقة",
                    GhafwaDiscribe=GetString(Resource.String.m45)
                },
               new GhafwaDiscreption
                {
                    NotificationId=606060,
                    GhafwaTime=60 * 60000,
                    GhafwaString="غفوة الـ60 دقيق",
                    GhafwaContent="سيرن الهاتف بعد 60 دقيقة من الآن",
                    EndNotificationString="لقد انتهت مدة غفوة ال60 دقيقة",
                    GhafwaDiscribe=GetString(Resource.String.m60)
                },
               new GhafwaDiscreption
                {
                    NotificationId=909090,
                    GhafwaTime=90 * 60000,
                    GhafwaString="غفوة الـ90 دقيق",
                    GhafwaContent="سيرن الهاتف بعد 90 دقيقة من الآن",
                    EndNotificationString="لقد انتهت مدة غفوة ال90 دقيقة",
                    GhafwaDiscribe=GetString(Resource.String.m90)
                },
            };

            ghafwaAdapter = new GhafwaAdapter(this,ghafwaDiscreptions);
            ghafwaListView.Adapter = ghafwaAdapter;

        }
    }
}