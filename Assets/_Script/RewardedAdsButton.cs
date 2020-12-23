using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

[RequireComponent(typeof(Button))]
public class RewardedAdsButton : MonoBehaviour, IUnityAdsListener
{

#if UNITY_IOS
    public string gameId = "3510889";
#elif UNITY_ANDROID
    public string gameId = "3510888";
#endif

    Button myButton;
    public string myPlacementId = "rewardedVideo";

    public int extraTimeReward = 10;

    void Start()
    {
        myButton = GetComponent<Button>();

        // Set interactivity to be dependent on the Placement’s status:
        myButton.interactable = Advertisement.IsReady(myPlacementId);

        // Map the ShowRewardedVideo function to the button’s click listener:
        if (myButton) myButton.onClick.AddListener(ShowRewardedVideo);

        // Initialize the Ads listener and service:
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId, false);
    }

    // Implement a function for showing a rewarded video ad:
    void ShowRewardedVideo()
    {
        Advertisement.Show(myPlacementId);
        myButton.gameObject.SetActive(false);
        myButton.interactable = false;
    }

    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsReady(string placementId)
    {
        // If the ready Placement is rewarded, activate the button: 
        if (placementId == myPlacementId)
        {
            myButton.interactable = true;
        }
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        // Define conditional logic for each ad completion status:
        switch (showResult)
        {
            case ShowResult.Failed:
                Debug.LogWarning("The ad did not finish due to an error.");
                break;
            case ShowResult.Skipped:
                // Do not reward the user for skipping the ad.
                break;
            case ShowResult.Finished:
                // Dont reward them if it was just a tranistional ad
                if (placementId == "video") return;
                // Reward the user for watching the ad to completion.
                GameController.Instance.GameOverScreenActiveIs(false);
                GameController.Instance.SetGameTimer(extraTimeReward);
                GameController.Instance.SetIsGameOver(false);
                GameController.Instance.SetGameStarted(false);
                GameController.Instance.StartTheTimer();
                break;
            default:
                Debug.LogWarning("Something Terrible went wrong");
                break;
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        // Log the error.
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // Optional actions to take when the end-users triggers an ad.
    }
}