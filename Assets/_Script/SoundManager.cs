using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    private bool musicState;
    private bool soundState;

    [Header("Sound/Music Related")]
    public Sprite musicOn;
    public Sprite musicOff;
    public Sprite soundOn;
    public Sprite soundOff;
    public Button musicButton;
    public Button soundButton;

    // Start is called before the first frame update
    void Start()
    {
        musicState = PlayerPrefs.GetInt(GameStrings.musicState, 1) == 1 ? true : false;
        soundState = PlayerPrefs.GetInt(GameStrings.soundState, 1) == 1 ? true : false;
        SetupSoundOnStart();
    }

    /// <summary>
    /// Turn on or off the music of the game, 1 music is turned on, 0 music is turned off
    /// </summary>
    public void TurnOnOffMusic()
    {
        // If the music value is 1, then musicState is true, otherwise its false
        if (musicState == true)
        {
            // Turn music off
            PlayerPrefs.SetInt(GameStrings.musicState, 0);
            musicButton.image.sprite = musicOff;
            musicState = false;
            AudioManager.instance.MusicController(false);
        }
        else
        {
            // Turn music on
            PlayerPrefs.SetInt(GameStrings.musicState, 1);
            musicButton.image.sprite = musicOn;
            musicState = true;
            AudioManager.instance.MusicController(true);
        }
    }

    /// <summary>
    /// Turn on or off the sound of the game, 1 sound is turned on, 0 sound is turned off
    /// </summary>
    public void TurnOnOffSound()
    {
        // If the sound value is 1, then soundState is true, otherwise its false
        if (soundState == true)
        {
            // Turn sound off
            PlayerPrefs.SetInt(GameStrings.soundState, 0);
            soundButton.image.sprite = soundOff;
            soundState = false;
            AudioManager.instance.SoundController(false);
        }
        else
        {
            // Turn sound on
            PlayerPrefs.SetInt(GameStrings.soundState, 1);
            soundButton.image.sprite = soundOn;
            soundState = true;
            AudioManager.instance.SoundController(true);
            AudioManager.instance.Play(GameStrings.buttonsClickedSound);
        }
    }

    /// <summary>
    /// Sets up wheather the music and sound are turned on or not
    /// </summary>
    public void SetupSoundOnStart()
    {
        // Play music and turn them on if its turned on
        if (musicState == true)
        {
            musicButton.image.sprite = musicOn;
            AudioManager.instance.MusicController(true);
        }
        else // otherwise silence them
        {
            musicButton.image.sprite = musicOff;
            AudioManager.instance.MusicController(false);
        }

        // Play sound effects if its turned on
        if (soundState == true)
        {
            soundButton.image.sprite = soundOn;
            AudioManager.instance.SoundController(true);
        }
        else // otherwise silence them
        {
            soundButton.image.sprite = soundOff;
            AudioManager.instance.SoundController(false);
        }
    }
}
