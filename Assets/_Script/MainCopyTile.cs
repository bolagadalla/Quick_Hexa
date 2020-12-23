using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCopyTile : MonoBehaviour
{
    public int rotateAtCombo;
    private Animator myAnimator;
    private int rotatedCombo = 0;
    private int difficultyLevel;

    void Start()
    {
        rotateAtCombo = Random.Range(3, 6);
        myAnimator = GetComponent<Animator>();
        difficultyLevel = PlayerPrefs.GetInt(GameStrings.playerDifficulty, 1);
    }

    void Update()
    {
        // Makes sure we dont rotate at the start of the game, and we didnt rotate already, and the remainder of the equation is 0
        if ((GameController.Instance.GetCorrectCombo() != 0) && (GameController.Instance.GetCorrectCombo() != rotatedCombo) && GameController.Instance.GetCorrectCombo() % rotateAtCombo == 0)
        {
            // Showing that we already ran it
            rotatedCombo = GameController.Instance.GetCorrectCombo();
            // Picks a random animation
            int randomAnimation = Random.Range(1, 5);
            myAnimator.SetInteger(GameStrings.mainCopyTileRotationAnimation, randomAnimation);
        }
    }

    /// <summary>
    /// Once they animation reaches the event point, it calls this function.
    /// This function freezes the animation and pauses for a random amount of seconds until it plays again
    /// </summary>
    /// <param name="minSeconds">Min seconds, set in the animation event</param>
    public void TilesRotation(int minSeconds)
    {
        myAnimator.enabled = false;
        StartCoroutine(PauseRotation(minSeconds));
    }

    /// <summary>
    /// Pauses for a certain amount of seconds
    /// </summary>
    /// <param name="minSeconds">Minimum amount of seconds to freeze</param>
    /// <returns>Waits a random amount of seconds</returns>
    IEnumerator PauseRotation(int minSeconds)
    {
        int randomTime;
        switch (difficultyLevel)
        {
            //HARD
            case 3:
                randomTime = Random.Range(minSeconds, 9);
                break;
            //MEDIUM
            case 2:
                randomTime = Random.Range(minSeconds, 7);
                break;
            //HARD
            default:
                randomTime = Random.Range(minSeconds, 5);
                break;
        }
        yield return new WaitForSeconds(randomTime);
        if (GameController.Instance.GetIsGameOver() == true) yield return null;
        myAnimator.enabled = true;
        myAnimator.SetInteger(GameStrings.mainCopyTileRotationAnimation, 0);
    }
}
