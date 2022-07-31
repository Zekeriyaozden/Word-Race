using System;
using System.Collections.Generic;
using UnityEngine;

namespace ATT
{
    public class ATTInit : MonoBehaviour
    {
        private void Awake()
        {
            ATTHelper.Start();
        }

    }
}



