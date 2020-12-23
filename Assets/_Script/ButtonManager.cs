using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class ButtonManager : MonoBehaviour
{
    private int amountOfGames;
    private bool musicState;
    private bool soundState;
    [Header("Screens")]
    public GameObject homeScreen;
    public GameObject playScreen;
    public GameObject pauseScreen;
    public GameObject pauseButton;
    public GameObject settingScreen;
    public GameObject statsScreen;
    public GameObject levelSelector;
    public GameObject howToPlayScreen;

    SoundManager soundManager;

    private void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
    }

    /// <summary>
    /// Go to the scene that the player selected in the level selector
    /// </summary>
    /// <param name="sceneIndex">Scene index in the build setting</param>
    public void TAPTOPLAYPressed()
    {
        AudioManager.instance.Play(GameStrings.buttonsClickedSound);
        int selectedLevel = PlayerPrefs.GetInt(GameStrings.selectedLevel, 1);
        // Counting how many games they played the game
        amountOfGames = PlayerPrefs.GetInt(GameStrings.playerAmountGamesPlayed, 0);
        PlayerPrefs.SetInt(GameStrings.playerAmountGamesPlayed, amountOfGames + 1);

        // Scene index should be the kind of level the user wants to go to
        SceneManager.LoadScene(selectedLevel);
    }

    /// <summary>
    /// Pauses the game by stoping the time and showing the pause screen
    /// </summary>
    public void PauseButtonPressed()
    {
        AudioManager.instance.Play(GameStrings.buttonsClickedSound);
        if ((pauseScreen.activeInHierarchy == false) && GameController.Instance.GetGameStarted() == true)
        {
            pauseScreen.SetActive(true);
            pauseButton.SetActive(false);
            GameController.Instance.SetIsGamePaused(true);
            Time.timeScale = 0;
        }
    }

    /// <summary>
    /// Resume the game by starting the time and deactivating the pause screen
    /// </summary>
    public void ResumeButtonPressed()
    {
        Time.timeScale = 1;
        AudioManager.instance.Play(GameStrings.buttonsClickedSound);
        pauseScreen.SetActive(false);
        pauseButton.SetActive(true);
        GameController.Instance.SetIsGamePaused(false);
    }

    /// <summary>
    /// Reloads the current level
    /// </summary>
    public void ReloadLevel()
    {
        Time.timeScale = 1;
        amountOfGames = PlayerPrefs.GetInt(GameStrings.playerAmountGamesPlayed, 0);
        // Counting how many games they played the game
        PlayerPrefs.SetInt(GameStrings.playerAmountGamesPlayed, amountOfGames + 1);
        AudioManager.instance.Play(GameStrings.buttonsClickedSound);
        if (amountOfGames % 5 == 0) Advertisement.Show("video");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Go to the home scene, it must be index 0 in the build setting
    /// </summary>
    public void GoHome()
    {
        Time.timeScale = 1;
        AudioManager.instance.Play(GameStrings.buttonsClickedSound);
        SceneManager.LoadScene(0);
    }

    public void SettingButtonPressed()
    {
        AudioManager.instance.Play(GameStrings.buttonsClickedSound);
        settingScreen.SetActive(true);
    }

    public void StatsButtonPressed()
    {
        AudioManager.instance.Play(GameStrings.buttonsClickedSound);
        statsScreen.SetActive(true);
    }

    public void LevelSelectorPressed()
    {
        AudioManager.instance.Play(GameStrings.buttonsClickedSound);
        levelSelector.SetActive(true);
    }

    public void CloseScreenPressed(GameObject screenToClose)
    {
        AudioManager.instance.Play(GameStrings.buttonsClickedSound);
        screenToClose.SetActive(false);
    }

    public void OpenHowToPlayScreen()
    {
        howToPlayScreen.SetActive(true);
    }

    public void ResetStats()
    {
        AudioManager.instance.Play(GameStrings.buttonsClickedSound);
        PlayerPrefs.DeleteKey(GameStrings.playerAmountGamesPlayed);
        PlayerPrefs.DeleteKey(GameStrings.playerHighscore);
        PlayerPrefs.DeleteKey(GameStrings.tilesPressed);
        PlayerPrefs.DeleteKey(GameStrings.amountOfTimePlayed);
        FindObjectOfType<PlayerPrefManager>().SetElementsToPlayerPrefs();
    }
}
