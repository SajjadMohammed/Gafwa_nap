
using Android.App;
using Android.Content;

namespace Gafwa
{
    public class RateClass 
    {
        public void RateApp()
        {
            string appPackageName = Application.Context.PackageName;
            try
            {
                Inputs.Uri = Android.Net.Uri.Parse("market://details?id=" + appPackageName);
                var intent = new Intent(Intent.ActionView, Inputs.Uri);
                // we need to add this, because the activity is in a new context.
                // Otherwise the runtime will block the execution and throw an exception
                intent.AddFlags(ActivityFlags.NewTask);

                Application.Context.StartActivity(intent);
            }
            catch (ActivityNotFoundException)
            {
                Inputs.Uri = Android.Net.Uri.Parse("http://play.google.com/store/apps/details?id=" + appPackageName);
                var intent = new Intent(Intent.ActionView, Inputs.Uri);
                // we need to add this, because the activity is in a new context.
                // Otherwise the runtime will block the execution and throw an exception
                intent.AddFlags(ActivityFlags.NewTask);

                Application.Context.StartActivity(intent);
            }
        }
    }
}