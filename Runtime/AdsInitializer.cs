using System;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;

namespace JPackage.AdsFramework
{
    public class AdsInitializer : MonoBehaviour
    {
        //for setting debug on/off.
        private static bool debugger;
        private TestDeviceConfigData testDeviceConfigData;

        //variable that take vlaues form insprector.
        [SerializeField] internal bool isDebugOn;

        //action to  notify for nitialization of ads.
        public static Action AdsInitComplete = delegate { };

        [Obsolete]
        private void Start()
        {
            debugger = isDebugOn;
            MobileAds.RaiseAdEventsOnUnityMainThread = true;
            testDeviceConfigData = (TestDeviceConfigData) TestDeviceConfigData.GetConfig();
            testDeviceConfigData.ConfigTestDevices();
            AdsInit();
        }

        /// <summary>
        /// Initialise Mobile Ads.
        /// </summary>
        private void AdsInit()
        {
            // Initialize the Google Mobile Ads SDK.
            MobileAds.Initialize((InitializationStatus initStatus) =>
            {
                // This callback is called once the MobileAds SDK is initialized.
                if (AdsInitComplete != null)
                    AdsInitComplete.Invoke();
            });
        }

        /// <summary>
        /// Prind log based on bool "Debugger".
        /// </summary>
        /// <param name="message"></param>
        internal static void PrintLog(string message)
        {
            if (debugger)
            {
                Debug.Log(message);
            }
        }
    }
}