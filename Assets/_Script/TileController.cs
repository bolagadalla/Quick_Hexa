using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TileController : MonoBehaviour
{
    [Header("GameObjects")]
    public GameObject mainCopyTileParent;
    public GameObject[] tilesToCopy;
    public GameObject[] mainTiles;
    [Space]
    public int activationPercentage = 6;

    private bool[] tilesActivated;
    private bool[] tilesPressed;
    private int numberOfTilesPressed = 0;
    private int numberOfTilesActivated = 0;
    private int numberOfTimesPressed = 0;
    private Color grayColor = new Color(0.1529412f, 0.1529412f, 0.1529412f, 1);

    GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        tilesActivated = new bool[mainTiles.Length];
        tilesPressed = new bool[mainTiles.Length];
        gameController = GetComponent<GameController>();
    }

    /// <summary>
    /// Resets the array to be all false and all gray
    /// </summary>
    public void ResetArray()
    {
        for (int i = 0; i < tilesActivated.Length; i++)
        {
            tilesActivated[i] = false;
            tilesPressed[i] = false;
            mainTiles[i].GetComponent<SpriteRenderer>().color = grayColor;
            tilesToCopy[i].GetComponent<SpriteRenderer>().color = grayColor;
            numberOfTilesActivated = 0;
            numberOfTilesPressed = 0;
            numberOfTimesPressed = 0;
        }
    }

    /// <summary>
    /// It makes sure it randomizes the tileActivated Array
    /// </summary>
    public void RandomizeArray()
    {
        // Randomize the boolean array tilesActivated
        for (int i = 0; i < tilesActivated.Length; i++)
        {
            // Gets a random number between 0 and 100
            int randomNumber = Random.Range(2, 10);
            if (activationPercentage <= randomNumber)
            {
                // Will be used to set this specific tile to be white
                tilesActivated[i] = true;
                // Assigns the tileActivated value to the tilesToCopy array as a color, white is true, gray is false
                AssignTilesFromArray(i);
                numberOfTilesActivated++;
            }
        }
        // if there is NO Activated Tiles then restart the function
        if (numberOfTilesActivated == 0) RandomizeArray();
    }

    /// <summary>
    /// Making the tilesToCopy array index White coressponding to the tilesActivated array
    /// </summary>
    private void AssignTilesFromArray(int currentIndex)
    {
        // White out the tiles corresponding to the index of array
        tilesToCopy[currentIndex].GetComponent<SpriteRenderer>().color = Color.white;
    }

    /// <summary>
    /// This is the main game loop, resets the array, randomize the array, assen the randomized array to the tiles
    /// </summary>
    private void NextTileSet()
    {
        ResetArray();
        RandomizeArray();
        gameController.AddExtraTime(1);
        gameController.AddScore(1);
    }

    /// <summary>
    /// Checks if both array have the same values at each index as the other
    /// </summary>
    /// <returns>Returns true if they are equal, Returns false if they are not equal</returns>
    private bool CheckTiles()
    {
        for (int i = 0; i < tilesActivated.Length; i++)
        {
            // When we check, it was wrong then we set the Correct Combo to 0 because they got it wrong
            if (tilesActivated[i] != tilesPressed[i])
            {
                return false;
            }
        }
        // Resets the counter to 1 once the player presses extra buttons meaning they didnt get a perfect pressing
        if (numberOfTimesPressed > numberOfTilesActivated) gameController.correctCombo = 0;
        else gameController.correctCombo++;
        return true;
    }

    /// <summary>
    /// When the tile is clicked then set the tilePressed at i to be true, otherwise to be false
    /// </summary>
    /// <param name="i">The index i of the tilesPressed Array</param>
    public void TileClickedAtIndex(int i)
    {
        // If its grayed out and not clicked before
        if (tilesPressed[i] == false)
        {
            tilesPressed[i] = true;
            mainTiles[i].GetComponent<SpriteRenderer>().color = Color.white;
            numberOfTilesPressed++;
        }
        // Otherwise if its already true
        else
        {
            tilesPressed[i] = false;
            mainTiles[i].GetComponent<SpriteRenderer>().color = grayColor;
            numberOfTilesPressed--;
        }

        // Checks if we plotted the correct tiles or not
        if (numberOfTilesPressed == numberOfTilesActivated)
        {
            if (CheckTiles())
            {
                StartCoroutine(PressedTilesPause());
            }
        }
    }

    /// <summary>
    /// Sets the value of the numberOfTimesPressed varaible to be +1
    /// </summary>
    public void SetNumberOfTimesPressed()
    {
        numberOfTimesPressed++;
    }

    /// <summary>
    /// Pauses for a fraction of a second and then resets everything
    /// </summary>
    /// <returns>Waits for 0.1f seconds until resets the board</returns>
    IEnumerator PressedTilesPause()
    {
        yield return new WaitForSeconds(0.1f);

        NextTileSet();
    }
}
