using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int index;
    private int tilesPressed;
    private Animator myAnimator;
    TileController tileController;
    SoundManager soundManager;

    void Start()
    {
        myAnimator = transform.parent.GetComponent<Animator>();
        tileController = GameController.Instance.gameObject.GetComponent<TileController>();
        soundManager = FindObjectOfType<SoundManager>();
    }

    private void OnMouseUp()
    {
        tilesPressed = PlayerPrefs.GetInt(GameStrings.tilesPressed, 0);
        // Makes sure the game is not over
        if ((GameController.Instance.GetIsGameOver() == true) || (GameController.Instance.GetGameStarted() == false) ||(GameController.Instance.GetIsGamePaused() == true)) return;
        myAnimator.SetTrigger(GameStrings.tilesClickedAnimation);
        // Increasing the number of times a player presses, this is to deal with the combo
        tileController.SetNumberOfTimesPressed();
        // Turns on the tiles the player presses and set it active in the array
        tileController.TileClickedAtIndex(index);
        AudioManager.instance.Play(GameStrings.tilesClickedSound);
        PlayerPrefs.SetInt(GameStrings.tilesPressed, tilesPressed + 1);
    }
}
