using Android.App;
using Android.Content;
using Android.Support.V4.App;

namespace Gafwa
{
    [BroadcastReceiver(Enabled = true, Name = "com.fajr.Gafwa.AlarmReceiver", Exported = false)]
	public class AlarmReceiver : BroadcastReceiver 
	{
		PendingIntent pi;
		NotificationManager nMngr;
		int notificationId;
		string title, content;
        Android.Net.Uri alarmSound;


        public override void OnReceive(Context context, Intent intent)
		{
			notificationId = intent.GetIntExtra("nid", 0);
			title = intent.GetStringExtra("title");
			content = intent.GetStringExtra("content");
			var cx = context;
			//var ringuri = intent.GetStringExtra("ringuri");
			var alarmIntent = new Intent(cx, typeof(GhafwaActivity));
			alarmIntent.AddFlags(ActivityFlags.ClearTop);

            if (notificationId == 111)
            {
                alarmSound = Android.Net.Uri.Parse("android.resource://" + cx.PackageName + "/" + Resource.Raw.startGhafwa);
            }
            else
            {
                alarmSound = Android.Net.Uri.Parse("android.resource://" + cx.PackageName + "/" + Resource.Raw.beautiful_sound);
            }
			pi = PendingIntent.GetActivity(cx, notificationId, alarmIntent, 0);
			var nBuilder = new NotificationCompat.Builder(cx)
												.SetContentTitle(title)
												.SetContentText(content)
												.SetSmallIcon(Resource.Drawable.notify)
												.SetContentIntent(pi)
												.SetAutoCancel(true)
												.Build();
            nBuilder.Sound = alarmSound;
			nMngr = cx.GetSystemService(Context.NotificationService) as NotificationManager;
			nMngr.Notify(notificationId, nBuilder);
		}
		protected override void Dispose(bool disposing)
		{
			nMngr.Cancel(notificationId);
			base.Dispose(disposing);
		}
	}
}