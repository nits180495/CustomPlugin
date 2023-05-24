using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JPackage.AdsFramework
{

    public enum AdsConfigType
    {
        appId,
        bannerAds,
        interstitialAds,
        rewardAds
    }

    [CreateAssetMenu(fileName = "AdsConfigDataSO", menuName = "AdsFramework/AdsConfigData")]
    public class AdsConfigData : ScriptableObject
    {
        public const string ADS_DATA_CONST = "AdsFramework/AdsConfigDataSO";

        //for managing live and test ID's.
        [SerializeField] internal bool isLive;

        [SerializeField] internal List<AdUnitId> adUnitIds = new List<AdUnitId>();

        /// <summary>
        /// Return Scriptable Object form Resources Folder.
        /// </summary>
        /// <returns></returns>
        public static System.Object GetConfig()
        {
            return Resources.Load(ADS_DATA_CONST);
        }

        /// <summary>
        /// Return ID's based on config.
        /// </summary>
        /// <returns></returns>
        public string GetID(AdsConfigType adsConfigType)
        {            
#if UNITY_ANDROID

            if(!isLive)
                return GetForElement(adsConfigType).TestEndPoint.androidAdUnitId;
            else
                return GetForElement(adsConfigType).productionEndPoint.androidAdUnitId;

#elif UNITY_IOS

            if(!isLive)
                return  GetForElement(adsConfigType).TestEndPoint.iosAdUnUnitId;
            else
                return  GetForElement(adsConfigType).productionEndPoint.iosAdUnUnitId;
#else
            return null;
#endif

        }

        private AdUnitId GetForElement(AdsConfigType adsConfigType)
        {
            AdUnitId _adUnitId = adUnitIds.Find(x => x.adsConfigType == adsConfigType);

            return _adUnitId;
        }
    }

    [Serializable]
    public class AdUnitId
    {
        public string Title;
        public AdsConfigType adsConfigType;
        public AdUnitType productionEndPoint;
        public AdUnitType TestEndPoint;
    }

    [Serializable]
    public class AdUnitType
    {
        public string androidAdUnitId;
        public string iosAdUnUnitId;
    }
}