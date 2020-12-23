using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerPrefManager : MonoBehaviour
{
    private int playerDifficulty;
    private int playerHighscore;
    private int playerAmountGamesPlayed;
    private int tilesPressed;
    private int amountOfTimePlayed; // In seconds

    public Slider difficultySlider;
    public TextMeshProUGUI currentDifficultyModeText;
    public TextMeshProUGUI playerHighscoreText;
    public TextMeshProUGUI playerAmountGamesPlayedText;
    public TextMeshProUGUI tilesPressedText;
    public TextMeshProUGUI amountOfTimePlayedText;

    // Start is called before the first frame update
    void Start()
    {
        SetElementsToPlayerPrefs();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerDifficulty == difficultySlider.value) return;
        SettingPlayerDifficulty();
    }

    private void GetAllPlayerPrefs()
    {
        playerDifficulty = PlayerPrefs.GetInt(GameStrings.playerDifficulty, 1);
        playerHighscore = PlayerPrefs.GetInt(GameStrings.playerHighscore, 0);
        playerAmountGamesPlayed = PlayerPrefs.GetInt(GameStrings.playerAmountGamesPlayed, 0);
        tilesPressed = PlayerPrefs.GetInt(GameStrings.tilesPressed, 0);
        amountOfTimePlayed = PlayerPrefs.GetInt(GameStrings.amountOfTimePlayed, 0);
    }

    public void SetElementsToPlayerPrefs()
    {
        GetAllPlayerPrefs();
        difficultySlider.value = playerDifficulty;
        currentDifficultyModeText.text = playerDifficulty == 1 ? "Easy" : playerDifficulty == 2 ? "Medium" : playerDifficulty == 3 ? "Hard" : "????";
        playerHighscoreText.text = playerHighscore.ToString();
        playerAmountGamesPlayedText.text = playerAmountGamesPlayed.ToString();
        tilesPressedText.text = tilesPressed.ToString();
        amountOfTimePlayedText.text = amountOfTimePlayed + " Seconds";
    }

    private void SettingPlayerDifficulty()
    {
        PlayerPrefs.SetInt(GameStrings.playerDifficulty, (int)difficultySlider.value);
        playerDifficulty = PlayerPrefs.GetInt(GameStrings.playerDifficulty, 1);
        AudioManager.instance.Play(GameStrings.buttonsClickedSound);
        SetElementsToPlayerPrefs();
    }
}
