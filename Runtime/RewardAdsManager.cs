using System;
using GoogleMobileAds.Api;
using UnityEngine;

namespace JPackage.AdsFramework
{
    public class RewardAdsManager : MonoBehaviour
    {
        //variable to hold rewardedAd.
        private RewardedAd rewardedAd;

        //Actions to notify what happend after reward Ad is created.
        public static Action<string, string, double> GrantRewardEvent = delegate { };
        public static Action OnAdOpeningEvent = delegate { };
        public static Action OnAdFailedToShowEvent = delegate { };
        public static Action OnAdClosedEvent = delegate { };

        /// <summary>
        /// Loads the rewarded ad.
        /// </summary>
        public void LoadRewardedAd(string adsUnitID)
        {
            // Clean up the old ad before loading a new one.
            if (rewardedAd != null)
            {
                DestroyAd();
            }

            AdsInitializer.PrintLog("Loading the rewarded ad.");
            // create our request used to load the ad.
            var _adRequest = new AdRequest.Builder().Build();

            // send the request to load the ad.
            RewardedAd.Load(adsUnitID, _adRequest,
                (RewardedAd _ad, LoadAdError _error) =>
                {
                    // if error is not null, the load request failed.
                    if (_error != null || _ad == null)
                    {
                        AdsInitializer.PrintLog("Rewarded ad failed to load an ad " +
                                       "with error : " + _error);
                        return;
                    }

                    AdsInitializer.PrintLog("Rewarded ad loaded with response : "
                              + _ad.GetResponseInfo());

                    rewardedAd = _ad;

                    RegisterEventHandlers(rewardedAd);
                });
        }

        /// <summary>
        /// Show Reward ads on screen.
        /// </summary>
        public void ShowRewardedAd()
        {
            const string _REWARD_MSG =
                "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";

            if (rewardedAd != null && rewardedAd.CanShowAd())
            {
                rewardedAd.Show((Reward reward) =>
                {
                    // TODO: Reward the user.
                    AdsInitializer.PrintLog(String.Format(_REWARD_MSG, reward.Type, reward.Amount));
                    if (GrantRewardEvent != null)
                        GrantRewardEvent.Invoke(_REWARD_MSG, reward.Type, reward.Amount);
                });
            }
        }


        /// <summary>
        /// Method to add listeners to evenets for Interstitial Ads.
        /// </summary>
        /// <param name="ad"></param>
        private void RegisterEventHandlers(RewardedAd ad)
        {
            // Raised when the ad is estimated to have earned money.
            ad.OnAdPaid += (AdValue adValue) =>
            {
                AdsInitializer.PrintLog(String.Format("Rewarded ad paid {0} {1}.",
                    adValue.Value,
                    adValue.CurrencyCode));
            };

            // Raised when an impression is recorded for an ad.
            ad.OnAdImpressionRecorded += () =>
            {
                AdsInitializer.PrintLog("Rewarded ad recorded an impression.");
            };

            // Raised when a click is recorded for an ad.
            ad.OnAdClicked += () =>
            {
                AdsInitializer.PrintLog("Rewarded ad was clicked.");
            };

            // Raised when an ad opened full screen content.
            ad.OnAdFullScreenContentOpened += () =>
            {
                AdsInitializer.PrintLog("Rewarded ad full screen content opened.");
                if (OnAdOpeningEvent != null)
                    OnAdOpeningEvent.Invoke();
            };

            // Raised when the ad closed full screen content.
            ad.OnAdFullScreenContentClosed += () =>
            {
                AdsInitializer.PrintLog("Rewarded Ad full screen content closed.");
                if (OnAdClosedEvent != null)
                    OnAdClosedEvent.Invoke();
            };

            // Raised when the ad failed to open full screen content.
            ad.OnAdFullScreenContentFailed += (AdError error) =>
            {
                AdsInitializer.PrintLog("Rewarded ad failed to open full screen content " +
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
            if (rewardedAd != null)
            {
                AdsInitializer.PrintLog("Destroying rewarded ad.");
                rewardedAd?.Destroy();
                rewardedAd = null;
            }
        }
    }
}