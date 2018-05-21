using Android.App;
using Android.Content;

namespace Gafwa
{
    public class GhafwaSettings
    {
        Context context;
        AlarmManager alarmManager;
        Intent intent;
        PendingIntent pendingIntent;
        public GhafwaSettings(Context context)
        {
            this.context = context;

        }

        public void StartAlarmBroadcastReceiver(long delay, long inc, bool isRepeat,int req,string title,string contet)
        {
            alarmManager = context.GetSystemService(Context.AlarmService) as AlarmManager;
            intent = new Intent(context, typeof(AlarmReceiver));
            intent.PutExtra("nid", req);
            intent.PutExtra("title", title);
            intent.PutExtra("content", contet);
            pendingIntent = PendingIntent.GetBroadcast(context, req, intent, 0);
            // Remove any previous pending intent.
            alarmManager.Cancel(pendingIntent);
            switch (isRepeat)
            {
                case true:
                    alarmManager.SetRepeating(AlarmType.RtcWakeup, delay, AlarmManager.IntervalDay, pendingIntent);
                    break;
                case false:
                    alarmManager.Set(AlarmType.RtcWakeup, delay + inc, pendingIntent);
                    break;
            }
        }

        public void StopAlarm(int req)
        {
            if (alarmManager == null) return;
            intent = new Intent(context, typeof(AlarmReceiver));
            pendingIntent = PendingIntent.GetBroadcast(context, req, intent, PendingIntentFlags.UpdateCurrent);
            pendingIntent.Cancel();
            alarmManager.Cancel(pendingIntent);
        }
    }
}