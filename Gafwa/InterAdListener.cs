
using Android.Gms.Ads;

namespace Gafwa
{
    public class InterAdListener : AdListener
    {
        InterstitialAd interstitialAds;
        public InterAdListener(InterstitialAd interstitialAd)
        {
            interstitialAds = interstitialAd;
        }
        public override void OnAdClosed()
        {
            base.OnAdClosed();
            interstitialAds.LoadAd(new AdRequest.Builder().Build());
        }
    }
}