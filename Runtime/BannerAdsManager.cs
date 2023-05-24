using System;
using GoogleMobileAds.Api;
using UnityEngine;

namespace JPackage.AdsFramework
{

    public class BannerAdsManager : MonoBehaviour
    {
        //variable to hold bannerview.
        private BannerView bannerView;

        public static int bannerWidth;
        public static string adKeyword;

        //Actions to notify what happend after banner ad is created.
        public static Action OnAdLoadedEvent = delegate { };
        public static Action OnAdOpeningEvent = delegate { };
        public static Action OnAdFailedToLoadEvent = delegate { };
        public static Action OnAdClosedEvent = delegate { };

        /// <summary>
        /// Creates a banner with given banner size at given postion of the screen.
        /// </summary>
        /// <param name="adUnitId"></param>
        /// <param name="bannerSize"></param>
        /// <param name="adPosition"></param>
        public void CreateBannerView(string adUnitId, AdSize.Type bannerSize, AdPosition adPosition)
        {
            AdsInitializer.PrintLog("Creating banner view");

            // If we already have a banner, destroy the old one.
            if (bannerView != null)
            {
                DestroyAd();
            }
            AdsInitializer.PrintLog("Creating banner view 2");

            bannerView = new BannerView(adUnitId, GetBannerSize(bannerSize), adPosition);
            AdsInitializer.PrintLog("Creating banner view 3");

            AddListnersToBannerView();
        }

        /// <summary>
        /// Creates a banner with given banner size at given coordinates on the screen.
        /// </summary>
        /// <param name="adUnitId"></param>
        /// <param name="bannerSize"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void CreateBannerView(string adUnitId, AdSize.Type bannerSize,int x, int y)
        {
            AdsInitializer.PrintLog("Creating banner view");

            // If we already have a banner, destroy the old one.
            if (bannerView != null)
            {
                DestroyAd();
            }

            bannerView = new BannerView(adUnitId, GetBannerSize(bannerSize), x, y);

            AddListnersToBannerView();
        }

        /// <summary>
        /// Return AdSize for banner. 
        /// </summary>
        /// <param name="bannerSize"></param>
        /// <returns></returns>
        private AdSize GetBannerSize(AdSize.Type bannerSize)
        {
            AdSize _adSize = AdSize.Banner;

            switch (bannerSize)
            {
                case AdSize.Type.Standard:
                    _adSize = AdSize.Banner;
                    break;

                case AdSize.Type.AnchoredAdaptive:
                    _adSize = AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(bannerWidth);
                    break;
            }
            return _adSize;
        }

        /// <summary>
        /// listen to events the banner may raise.
        /// </summary>
        private void AddListnersToBannerView()
        {
            // Raised when an ad is loaded into the banner view.
            bannerView.OnBannerAdLoaded += () =>
            {
                AdsInitializer.PrintLog("Banner ad loaded.");
                if (OnAdLoadedEvent != null)
                    OnAdLoadedEvent.Invoke();
            };

            // Raised when an ad fails to load into the banner view.
            bannerView.OnBannerAdLoadFailed += (LoadAdError error) =>
            {
                AdsInitializer.PrintLog("Banner ad failed to load with error: " + error.GetMessage());
                if (OnAdFailedToLoadEvent != null)
                    OnAdFailedToLoadEvent.Invoke();
            };

            // Raised when the ad is estimated to have earned money.
            bannerView.OnAdPaid += (AdValue adValue) =>
            {
                AdsInitializer.PrintLog(String.Format("Banner view paid {0} {1}.",
                    adValue.Value,
                    adValue.CurrencyCode));
            };

            // Raised when an impression is recorded for an ad.
            bannerView.OnAdImpressionRecorded += () =>
            {
                AdsInitializer.PrintLog("Banner view recorded an impression.");
            };

            // Raised when a click is recorded for an ad.
            bannerView.OnAdClicked += () =>
            {
                AdsInitializer.PrintLog("Banner view was clicked.");
            };

            // Raised when an ad opened full screen content.
            bannerView.OnAdFullScreenContentOpened += () =>
            {
                AdsInitializer.PrintLog("Banner view full screen content opened.");
                if (OnAdOpeningEvent != null)
                    OnAdOpeningEvent.Invoke();
            };

            // Raised when the ad closed full screen content.
            bannerView.OnAdFullScreenContentClosed += () =>
            {
                AdsInitializer.PrintLog("Banner view full screen content closed.");
                if (OnAdClosedEvent != null)
                    OnAdClosedEvent.Invoke();
            };
        }


        /// <summary>
        /// Creates the banner view and loads a banner ad.
        /// </summary>
        public void ShowBannerAd()
        {
            AdsInitializer.PrintLog("Creating banner view 4");

            // create our request used to load the ad.
            var _adRequest = new AdRequest.Builder()
                .AddKeyword(adKeyword)
                .Build();
            AdsInitializer.PrintLog("Creating banner view 5");

            // send the request to load the ad.
            AdsInitializer.PrintLog("Loading banner ad.");
            bannerView.LoadAd(_adRequest);
            AdsInitializer.PrintLog("Creating banner view 6");

        }

        /// <summary>
        /// Destroys the ad.
        /// </summary>
        public void DestroyAd()
        {
            if (bannerView != null)
            {
                AdsInitializer.PrintLog("Destroying banner ad.");
                bannerView.Destroy();
                bannerView = null;
            }
        }
    }
}
