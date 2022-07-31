using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ATTSettings", menuName = "ATT/ATTSettings")]
public class ATTSettings : ScriptableObject
{
    [Header("SKAdNetwork")]
    [SerializeField] private SKAdNetworkName identifiers;
    [SerializeField] private TextAsset identifiersJson;

    [Header("Attribution")]
    [Tooltip("Register App for Network Attribution")]
    [SerializeField] private bool registerForAttribution = true;

    [Header("ATT")]
    [SerializeField] private bool enableATT = true;
    [SerializeField] private bool firebaseEvents = true;
    [SerializeField] private bool facebookFlag = true;

    public const string TrackingDescription = "Your data will be used to improve your in-game experience.";

    public SKAdNetworkName Identifiers => identifiers;
    public TextAsset IdentifiersJson => identifiersJson;
    public bool RegisterForAttribution => registerForAttribution;
    public bool EnableATT => enableATT;
    public bool FirebaseEvents => firebaseEvents;
    public bool FacebookFlag => facebookFlag;
}

[Flags]
public enum SKAdNetworkName
{
    ironSource = 1,
    AdColony = 2,
    AdMob = 4,
    AppLovin = 8,
    Chartboost = 16,
    Facebook = 32,
    Fyber = 64,
    HyprMx = 128,
    InMobi = 256,
    Maio = 512,
    Pangle = 1024,
    Snap = 2048,
    Tapjoy = 4096,
    UnityAds = 8192,
    Vungle = 16384
}