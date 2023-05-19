using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JPackage.AdsFramework;
using UnityEngine.UI;
using GoogleMobileAds.Api;

public class SampleAdManager : MonoBehaviour
{
    private AdsConfigData adsConfigData;

    #region Banner Ads Variables
    [SerializeField] private BannerAdsManager bannerAdsManager;
    [SerializeField] private Button WatchBannerAdsButton;
    [SerializeField] private Button CloseBannerAdsButton;

    #endregion Banner Ads Variables

    #region Interstitial Ads Variables

    [SerializeField] private InterstitialAdsManager interstitialAdsManager;
    [SerializeField] private Button WatchInterstitialAdsButton;

    #endregion Interstitial Ads Variables

    #region Reward Ads Variables

    [SerializeField] private RewardAdsManager rewardAdsManager;
    [SerializeField] private Button WatchRewardAdsButton;

    #endregion Reward Ads Variables

    private void Awake()
    {
        adsConfigData = (AdsConfigData)AdsConfigData.GetConfig();
    }

    private void OnEnable()
    {
        WatchBannerAdsButton.onClick.AddListener(WatchBannerAds);
        CloseBannerAdsButton.onClick.AddListener(CloseBannerAds);
        WatchInterstitialAdsButton.onClick.AddListener(WatchInterstitialAds);
        WatchRewardAdsButton.onClick.AddListener(WatchRewardAds);

        InterstitialAdsManager.OnAdClosedEvent += () =>
        {
            WatchInterstitialAdsButton.interactable = true;
            interstitialAdsManager.DestroyAd();
        };
        RewardAdsManager.OnAdClosedEvent += () =>
        {
            WatchRewardAdsButton.interactable = true;
            rewardAdsManager.DestroyAd();
        };
    }

    private void OnDisable()
    {
        WatchBannerAdsButton.onClick.RemoveAllListeners();
        CloseBannerAdsButton.onClick.AddListener(CloseBannerAds);
        WatchInterstitialAdsButton.onClick.RemoveAllListeners();
        WatchRewardAdsButton.onClick.RemoveAllListeners();

        BannerAdsManager.OnAdClosedEvent -= () =>
        {
            WatchBannerAdsButton.interactable = true;
            bannerAdsManager.DestroyAd();
        };

        InterstitialAdsManager.OnAdClosedEvent -= () =>
        {
            WatchInterstitialAdsButton.interactable = true;
            interstitialAdsManager.DestroyAd();
        };
        RewardAdsManager.OnAdClosedEvent -= () =>
        {
            WatchRewardAdsButton.interactable = true;
            rewardAdsManager.DestroyAd();
        };
    }

    #region Banner Ads

    private void WatchBannerAds()
    {
        WatchBannerAdsButton.interactable = false;
        CloseBannerAdsButton.interactable = true;
        bannerAdsManager.CreateBannerView(adsConfigData.GetID(AdsConfigType.bannerAds), AdSize.Type.Standard,AdPosition.Top);
        bannerAdsManager.ShowBannerAd();
    }

    private void CloseBannerAds()
    {
        CloseBannerAdsButton.interactable = false;
        WatchBannerAdsButton.interactable = true;
        bannerAdsManager.DestroyAd();
    }

    #endregion Banner Ads

    #region Interstitial Ads

    private void WatchInterstitialAds()
    {
        WatchInterstitialAdsButton.interactable = false;
        interstitialAdsManager.LoadInterstitialAd(adsConfigData.GetID(AdsConfigType.interstitialAds));
        interstitialAdsManager.ShowInterstitialAd();
    }

    #endregion Interstitial Ads

    #region Reward Ads

    private void WatchRewardAds()
    {
        WatchRewardAdsButton.interactable = false;
        rewardAdsManager.LoadRewardedAd(adsConfigData.GetID(AdsConfigType.rewardAds));
        rewardAdsManager.ShowRewardedAd();
    }

    #endregion Reward Ads
}
