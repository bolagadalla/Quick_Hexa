using UnityEngine;
using UnityEngine.Advertisements;

public class InterstitialAdsScript : MonoBehaviour
{

    public string gameId = "1234567";
    bool testMode = false;

    void Start()
    {
        // Initialize the Ads service:
        Advertisement.Initialize(gameId, testMode);
        // Show an ad:
        Advertisement.Show();
    }
}