using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsInitializer : MonoBehaviour
{
#if UNITY_IOS
    private string gameId = "3510889";
#elif UNITY_ANDROID
    private string gameId = "3510888";
#endif

    public bool testMode = false;

    void Start()
    {
        Advertisement.Initialize(gameId, testMode);
    }
}
