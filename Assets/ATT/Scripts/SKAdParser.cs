using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace ATT
{
    [Serializable]
    public class SKAdItem
    {
        public SKAdNetworkName NameId;
        public string NameString;
        public List<string> Identifiers;
    }

    [Serializable]
    public class SKAdList
    {
        public List<SKAdItem> Data;
    }

    public class SKAdParser
    {
#if UNITY_EDITOR

        private ATTSettings settings;

        public SKAdParser(ATTSettings settings)
        {
            this.settings = settings;
        }

        public HashSet<string> Parse()
        {
            var identifiers = settings.Identifiers;
            var identifiersJson = settings.IdentifiersJson;

            var data = JsonUtility.FromJson<SKAdList>(identifiersJson.text);
            var set = new HashSet<string>();

            foreach (SKAdNetworkName id in Enum.GetValues(typeof(SKAdNetworkName)))
            {
                if (identifiers.HasFlag(id))
                {
                    Debug.Log($"{id} identifier set");
                    var item = data.Data.FirstOrDefault(x => x.NameId == id);
                    if (item == null)
                    {
                        Debug.LogError($"Identifier with id = {id} not found!");
                        continue;
                    }
                    else
                    {
                        foreach (var identifier in item.Identifiers)
                        {
                            set.Add(identifier);
                        }
                    }
                }
            }

            return set;
        }
#endif
    }
}
