using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using GoogleMobileAds.Api;
using UnityEngine;

namespace JPackage.AdsFramework
{
    [CreateAssetMenu(fileName = "TestDeviceIdsDataSO", menuName = "TestDeviceIdsData")]
    public class TestDeviceConfigData : ScriptableObject
    {
        public const string TEST_DEVICE_ID_DATA_CONST = "TestDeviceIdsDataSO";

        //Add device ID's to register those device for showing ads.
        [SerializeField] private List<string> deviceIds = new List<string>() { AdRequest.TestDeviceSimulator };

        /// <summary>
        /// Return Scriptable Object form Resources Folder.
        /// </summary>
        /// <returns></returns>
        public static Object GetConfig()
        {
            return Resources.Load(TEST_DEVICE_ID_DATA_CONST);
        }

        /// <summary>
        /// Config all test device ID's.
        /// </summary>
        public void ConfigTestDevices()
        {
#if UNITY_IOS
            MobileAds.SetiOSAppPauseOnBackground(true);
#endif
            RequestConfiguration requestConfiguration = new RequestConfiguration
                .Builder()
                .SetTestDeviceIds(deviceIds)
                .build();

            MobileAds.SetRequestConfiguration(requestConfiguration);
        }

        /// <summary>
        /// Add Test Device ID's in List.
        /// </summary>
        /// <param name="ids"></param>
        internal void AddDeviceIdsInList(List<string> ids)
        {
            foreach (var id in ids)
                deviceIds.Add(id);
        }
    }
}
