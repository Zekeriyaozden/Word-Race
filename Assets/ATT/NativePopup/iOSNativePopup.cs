using AOT;
using System.Runtime.InteropServices;
using UnityEngine;

namespace ATT
{
    public class iOSNativePopup
    {
#if UNITY_IOS

        [DllImport("__Internal")] private static extern void _ShowAlert(string title, string message, string button, AlertCompleteHandler callback);

#endif

        public delegate void AlertCompleteHandler();
        private static AlertCompleteHandler _alertCompleteCallback = null;

        public static void ShowAlert(string title, string message, string button, AlertCompleteHandler completeHandler)
        {
#if UNITY_IOS
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                _alertCompleteCallback = completeHandler;
                _ShowAlert(title, message, button, AlertCompleted);
            }
            else
#endif
            {
                Debug.LogWarning($"Not supported on this platform. Title: {title} message: {message}");
            }
        }

        /// <summary>
        /// The static callback that will be invoked from native code and invoke the callback"/>
        /// </summary>
        [MonoPInvokeCallback(typeof(AlertCompleteHandler))]
        public static void AlertCompleted()
        {
            _alertCompleteCallback?.Invoke();
            _alertCompleteCallback = null;
        }


    }
}