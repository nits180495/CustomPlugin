using System;
using GoogleMobileAds.Api;
using UnityEngine;

namespace JPackage.AdsFramework
{
    public class InterstitialAdsManager : MonoBehaviour
    {
        //variable to hold interstitialAd.
        private InterstitialAd interstitialAd;

        //Actions to notify what happend after interstitial Ad is created.
        public static Action OnAdOpeningEvent = delegate { };
        public static Action OnAdFailedToShowEvent = delegate { };
        public static Action OnAdClosedEvent = delegate { };

        /// <summary>
        /// Loads the interstitial ad.
        /// </summary>
        public void LoadInterstitialAd(string adsUnitID)
        {
            // Clean up the old ad before loading a new one.
            if (interstitialAd != null)
            {
                DestroyAd();
            }

            AdsInitializer.PrintLog("Loading the interstitial ad.");

            // create our request used to load the ad.
            var _adRequest = new AdRequest.Builder().Build();

            // send the request to load the ad.
            InterstitialAd.Load(adsUnitID, _adRequest,
                (InterstitialAd _ad, LoadAdError _error) =>
                {
                    // if error is not null, the load request failed.
                    if (_error != null || _ad == null)
                    {
                        AdsInitializer.PrintLog("interstitial ad failed to load an ad " +
                                       "with error : " + _error);
                        return;
                    }

                    AdsInitializer.PrintLog("Interstitial ad loaded with response : "
                              + _ad.GetResponseInfo());

                    interstitialAd = _ad;

                    AddListnersToInterstitialView(interstitialAd);
                });
        }

        /// <summary>
        /// Shows the interstitial ad.
        /// </summary>
        public void ShowInterstitialAd()
        {
            if (interstitialAd != null && interstitialAd.CanShowAd())
            {
                AdsInitializer.PrintLog("Showing interstitial ad.");
                interstitialAd.Show();
            }
            else
            {
                AdsInitializer.PrintLog("Interstitial ad is not ready yet.");
            }
        }

        /// <summary>
        /// Method to add listeners to evenets for Interstitial Ads.
        /// </summary>
        /// <param name="ad"></param>
        private void AddListnersToInterstitialView(InterstitialAd ad)
        {
            // Raised when the ad is estimated to have earned money.
            ad.OnAdPaid += (AdValue adValue) =>
            {
                AdsInitializer.PrintLog(String.Format("Interstitial ad paid {0} {1}.",
                    adValue.Value,
                    adValue.CurrencyCode));
            };

            // Raised when an impression is recorded for an ad.
            ad.OnAdImpressionRecorded += () =>
            {
                AdsInitializer.PrintLog("Interstitial ad recorded an impression.");
            };

            // Raised when a click is recorded for an ad.
            ad.OnAdClicked += () =>
            {
                AdsInitializer.PrintLog("Interstitial ad was clicked.");
            };

            // Raised when an ad opened full screen content.
            ad.OnAdFullScreenContentOpened += () =>
            {
                AdsInitializer.PrintLog("Interstitial ad full screen content opened.");
                if (OnAdOpeningEvent != null)
                    OnAdOpeningEvent.Invoke();
            };

            // Raised when the ad closed full screen content.
            ad.OnAdFullScreenContentClosed += () =>
            {
                AdsInitializer.PrintLog("Interstitial Ad full screen content closed.");
                if (OnAdClosedEvent != null)
                    OnAdClosedEvent.Invoke();
            };

            // Raised when the ad failed to open full screen content.
            ad.OnAdFullScreenContentFailed += (AdError error) =>
            {
                AdsInitializer.PrintLog("Interstitial ad failed to open full screen content " +
                               "with error : " + error);
                if (OnAdFailedToShowEvent != null)
                    OnAdFailedToShowEvent.Invoke();
            };
        }

        /// <summary>
        /// Destroys the ad.
        /// </summary>
        public void DestroyAd()
        {
            if (interstitialAd != null)
            {
                AdsInitializer.PrintLog("Destroying interstitial ad.");
                interstitialAd.Destroy();
                interstitialAd = null;
            }
        }
    }
}