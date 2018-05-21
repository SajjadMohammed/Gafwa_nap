using Android.Content;

namespace Gafwa
{
    public class Alerts
    {
        Context context;
        public Alerts(Context context)
        {
            this.context = context;
        }
        public void SetAlert(string title, string content)
        {
            var alertMngr = new Android.Support.V7.App.AlertDialog.Builder(context);
            alertMngr.SetTitle(title);
            alertMngr.SetMessage(content);
            alertMngr.SetPositiveButton("Ok",
                delegate
                {

                });
            alertMngr.Show();
        }
    }
}