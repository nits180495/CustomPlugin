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
    [SerializeField] private Button LoadInterstitialAdsButton;
    [SerializeField] private Button WatchInterstitialAdsButton;

    #endregion Interstitial Ads Variables

    #region Reward Ads Variables

    [SerializeField] private RewardAdsManager rewardAdsManager;
    [SerializeField] private Button LoadRewardAdsButton;
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
        LoadInterstitialAdsButton.onClick.AddListener(LoadInterstitialAds);
        WatchInterstitialAdsButton.onClick.AddListener(WatchInterstitialAds);
        LoadRewardAdsButton.onClick.AddListener(LoadRewardAds);
        WatchRewardAdsButton.onClick.AddListener(WatchRewardAds);

        InterstitialAdsManager.OnAdClosedEvent += () =>
        {
            LoadInterstitialAdsButton.interactable = true;
            interstitialAdsManager.DestroyAd();
        };
        RewardAdsManager.OnAdClosedEvent += () =>
        {
            LoadRewardAdsButton.interactable = true;
            rewardAdsManager.DestroyAd();
        };
    }

    private void OnDisable()
    {
        WatchBannerAdsButton.onClick.RemoveAllListeners();
        CloseBannerAdsButton.onClick.AddListener(CloseBannerAds);
        LoadInterstitialAdsButton.onClick.RemoveAllListeners();
        WatchInterstitialAdsButton.onClick.RemoveAllListeners();
        LoadRewardAdsButton.onClick.RemoveAllListeners();
        WatchRewardAdsButton.onClick.RemoveAllListeners();

        InterstitialAdsManager.OnAdClosedEvent -= () =>
        {
            LoadInterstitialAdsButton.interactable = true;
            interstitialAdsManager.DestroyAd();
        };
        RewardAdsManager.OnAdClosedEvent -= () =>
        {
            LoadRewardAdsButton.interactable = true;
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

    private void LoadInterstitialAds()
    {
        LoadInterstitialAdsButton.interactable = false;
        WatchInterstitialAdsButton.interactable = true;
        interstitialAdsManager.LoadInterstitialAd(adsConfigData.GetID(AdsConfigType.interstitialAds));
    }

    private void WatchInterstitialAds()
    {
        WatchInterstitialAdsButton.interactable = false;
        interstitialAdsManager.ShowInterstitialAd();
    }

    #endregion Interstitial Ads

    #region Reward Ads

    private void LoadRewardAds()
    {
        LoadRewardAdsButton.interactable = false;
        WatchRewardAdsButton.interactable = true;
        rewardAdsManager.LoadRewardedAd(adsConfigData.GetID(AdsConfigType.rewardAds));
    }

    private void WatchRewardAds()
    {
        WatchRewardAdsButton.interactable = false;
        rewardAdsManager.ShowRewardedAd();
    }

    #endregion Reward Ads
}
