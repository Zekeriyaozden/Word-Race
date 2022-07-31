using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
#if UNITY_IOS
using UnityEngine.iOS;
using Unity.Advertisement.IosSupport;
#endif

namespace ATT
{
    public class ATTHelper
    {
#if UNITY_IOS
        private static ATTrackingStatusBinding.RequestAuthorizationTrackingCompleteHandler requestCompleteHandler;
        private static iOSNativePopup.AlertCompleteHandler alertCompleteHandler;

        private const string LogOpen = "att_open";
        private const string LogAllow = "att_allow";

        private const string Header = "Improve your Experience";
        private const string Text = "Improve your experience and see ads that match your interests, allow tracking.";
        private const string Button = "Next";
#endif

        private static ATTSettings settings;

        public static void Start()
        {
            settings = Resources.Load<ATTSettings>("ATTSettings");

            if (settings.RegisterForAttribution)
            {
                RegisterAttribution();
            }

            if (settings.EnableATT)
            {
                AskForPermission();
            }

            if (settings.FacebookFlag)
            {
                CheckFacebook();
            }

        }

        private static void AskForPermission()
        {
#if UNITY_IOS
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                if (CheckMinVersion("14.5"))
                {
                    var status = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();
                    if (status == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
                    {
                        StartFlow();
                    }
                }
            }
#endif
        }

        private static void RegisterAttribution()
        {
#if UNITY_IOS
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                SkAdNetworkBinding.SkAdNetworkRegisterAppForNetworkAttribution();
            }
#endif
        }

        private static void CheckFacebook()
        {
#if UNITY_IOS
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                if (CheckMinVersion("14.5"))
                {
                    var status = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();
                    if (status == ATTrackingStatusBinding.AuthorizationTrackingStatus.AUTHORIZED)
                    {
                        SetFacebookState(true);                        
                    }
                }
                else if (CheckMinVersion("14.0"))
                {
                    SetFacebookState(true);
                }
            }
#endif
        }

        private static void SetFacebookState(bool state)
        {
#if UNITY_IOS
            /// For Facebook Audience Network
            /// Use this if you have Audience Network-supplied Unity wrapper
            /// For allowing advertisement to be personalized
            /// In another case just comment it
            /// Details: https://developers.facebook.com/docs/audience-network/setting-up/platform-setup/ios/advertising-tracking-enabled?locale=en_US#unity
            AudienceNetwork.AdSettings.SetAdvertiserTrackingEnabled(state);

            /// For Facebook’s SDK
            /// Just comment it if you don't have facebook SDK
            /// Details: https://developers.facebook.com/docs/app-events/guides/advertising-tracking-enabled
            FB.Mobile.SetAdvertiserTrackingEnabled(state);
#endif
        }


#if UNITY_IOS
        private static bool CheckMinVersion(string version)
        {
            var currentVersion = new Version(Device.systemVersion);
            var minimalVersion = new Version(version);
            var versionCompatible = currentVersion >= minimalVersion;
            return versionCompatible;
        }

        private static void StartFlow()
        {
            /// For Firebase Analytics
            /// Use this if you have Firebase for tracking events
            if (settings.FirebaseEvents) Firebase.Analytics.FirebaseAnalytics.LogEvent(LogOpen);

            ShowAlert();
        }

        private static void OnRequestComplete()
        {
            if (ATTrackingStatusBinding.GetAuthorizationTrackingStatus() ==
                ATTrackingStatusBinding.AuthorizationTrackingStatus.AUTHORIZED)
            {
                /// For Firebase Analytics
                /// Use this if you have Firebase for tracking events
                if (settings.FirebaseEvents) Firebase.Analytics.FirebaseAnalytics.LogEvent(LogAllow);                

                if (settings.FacebookFlag)
                {
                    SetFacebookState(true);
                }
            }
            else
            {
                if (settings.FacebookFlag)
                {
                    SetFacebookState(false);
                }
            }
        }


        private static void OnAlertComplete()
        {
            ShowRequest();
        }


        private static void ShowRequest()
        {
            requestCompleteHandler = OnRequestComplete;
            ATTrackingStatusBinding.RequestAuthorizationTracking(requestCompleteHandler);
        }


        private static void ShowAlert()
        {
            alertCompleteHandler = OnAlertComplete;
            iOSNativePopup.ShowAlert(Header, Text, Button, alertCompleteHandler);
        }

#endif

    }
}


