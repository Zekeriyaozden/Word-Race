using UnityEditor;
using UnityEditor.Callbacks;
using System.IO;
using UnityEngine;
using System.Collections.Generic;

#if UNITY_IOS
using UnityEditor.iOS.Xcode;
#endif

namespace ATT
{
    public class ATTPostBuild
    {
        private static ATTSettings settings;

        private const string k_SkAdNetworkItems = "SKAdNetworkItems";
        private const string k_SkAdNetworkIdentifier = "SKAdNetworkIdentifier";

        /// <summary>
        /// Description for IDFA request notification 
        /// [sets NSUserTrackingUsageDescription]
        /// </summary>
        

        [PostProcessBuild(0)]
        public static void OnPostprocessBuild(BuildTarget buildTarget, string pathToXcode)
        {
#if UNITY_IOS
            if (buildTarget == BuildTarget.iOS)
            {
                settings = Resources.Load<ATTSettings>("ATTSettings");
                AddPListValues(pathToXcode);
            }
#endif
        }

        private static void AddPListValues(string pathToXcode)
        {
#if UNITY_IOS
            // Get Plist from Xcode project 
            string plistPath = pathToXcode + "/Info.plist";

            // Read in Plist 
            PlistDocument plistObj = new PlistDocument();
            plistObj.ReadFromString(File.ReadAllText(plistPath));

            // set values from the root obj
            PlistElementDict plistRoot = plistObj.root;

            // Set value in plist
            // Set ATT popup
            plistRoot.SetString("NSUserTrackingUsageDescription", ATTSettings.TrackingDescription);

            // Set SKAdNetwork Identifiers
            plistRoot.CreateArray(k_SkAdNetworkItems);
            var adNetworkItems = plistRoot[k_SkAdNetworkItems].AsArray();
            var set = Parse();

            foreach (var adNetworkId in set)
            {
                adNetworkItems.AddDict().SetString(k_SkAdNetworkIdentifier, adNetworkId);
            }

            // save
            File.WriteAllText(plistPath, plistObj.WriteToString());
#endif
        }

        private static HashSet<string> Parse()
        {
            var SKAdParser = new SKAdParser(settings);
            var set = SKAdParser.Parse();
            return set;
        }
    }
}
