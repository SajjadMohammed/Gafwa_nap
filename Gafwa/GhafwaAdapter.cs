using System;
using System.Collections.Generic;
using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Widget;

namespace Gafwa
{
    public class GhafwaAdapter : BaseAdapter<GhafwaDiscreption>
    {
        readonly Context context;
        readonly List<GhafwaDiscreption> ghafwaDiscreptions;
        Alerts alerts;
        readonly GhafwaSettings ghafwaSettings;
        DateTime now;
        public GhafwaAdapter(Context context,  List<GhafwaDiscreption> ghafwaDiscreptions)
        {
            this.context = context;
            ghafwaSettings = new GhafwaSettings(context);
            this.ghafwaDiscreptions = ghafwaDiscreptions;
            alerts = new Alerts(context);
        }

        public override int Count => ghafwaDiscreptions.Count;

        public override GhafwaDiscreption this[int position] => ghafwaDiscreptions[position];

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = this[position]; //ghafwaDiscreptions[position];
            var view = convertView ?? LayoutInflater.From(context).Inflate(Resource.Layout.listItemLayout, parent, false);

            Button startButton = view.FindViewById<Button>(Resource.Id.startButton);
            TextView discTextView = view.FindViewById<TextView>(Resource.Id.discTextView);

            discTextView.Text = item.GhafwaDiscribe;
            TextFont(discTextView);
            startButton.Click += delegate
            {
                alerts.SetAlert(item.GhafwaString, item.GhafwaContent);
                SetGhafwa(item.NotificationId, item.GhafwaTime, item.GhafwaString, item.EndNotificationString);
            };
            ButtonFont(startButton);

            return view;
        }

        void SetGhafwa(int notificationId, long time, string title, string content)
        {
            now = DateTime.Now;

            Inputs.NotificationId = notificationId;
            Inputs.Title = title;
            Inputs.Content = content;

            ghafwaSettings.StartAlarmBroadcastReceiver(Java.Lang.JavaSystem.CurrentTimeMillis(), time, false, Inputs.NotificationId, Inputs.Title, Inputs.Content);
        }

        void TextFont(TextView textView) => textView.Typeface = Typeface.CreateFromAsset(context.Assets, "JF-Flat-regular.otf");
        void ButtonFont(Button button) => button.Typeface = Typeface.CreateFromAsset(context.Assets, "JF-Flat-regular.otf");

    }
}