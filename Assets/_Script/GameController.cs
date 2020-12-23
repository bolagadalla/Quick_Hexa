using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using TMPro;

public class GameController : MonoBehaviour
{
    private bool isGameOver = false;
    private bool gameStarted = false;
    private bool isGamePaused = false;
    private bool adPlayed;

    [Header("Variables")]
    public int correctCombo = -1;
    public int playAdEvery = 5; // Games
    public int startTimer = 30;
    private int playerScore = 0;
    private int countdown = 3;
    private int amountOfGames;
    private int amountOfTimePlayed;
    private int difficultyLevel;
    private float time = 1;

    [SerializeField] private float gameTime;

    private readonly string adPlacementID = "video";

    private TileController tileController;

    public GameObject gameoverScreen;
    public GameObject newHighscoreText;
    public MainCopyTile mainCopyTile;

    [Space]
    [Header("Animators Fields")]
    public Animator timerCountDownAnimator;
    public Animator countDownAnimator;
    public Animator addScoreAnimator;
    public Animator addTimeAnimator;

    [Space]
    [Header("TextArea Fields")]
    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI currentHighscoreText;
    public TextMeshProUGUI playerHighscore;
    public TextMeshProUGUI countdownText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;
    public TextMeshProUGUI addScoreText;
    public TextMeshProUGUI addTimerText;

    private static GameController instance;
    public static GameController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameController>();
            }
            return instance;
        }
    }

    void Start()
    {
        amountOfGames = PlayerPrefs.GetInt(GameStrings.playerAmountGamesPlayed);
        difficultyLevel = PlayerPrefs.GetInt(GameStrings.playerDifficulty, 1);
        gameTime = startTimer;
        tileController = GetComponent<TileController>();
        adPlayed = false;
        StartTheTimer();
    }

    void Update()
    {
        // Once its gameover save the player score if its higher then the player highscore
        if (GetIsGameOver() == true)
        {
            SavePlayerHighscore();
            GameOver();
            return;
        }
        if ((GetGameStarted() == false) || GetIsGamePaused() == true) return;
        mainCopyTile.gameObject.SetActive(true);
        SubtractTimer();
        UpdateEmements();
    }

    /// <summary>
    /// Subtracts 1 seconds every seconds
    /// </summary>
    private void SubtractTimer()
    {
        // Checks to see if there is no more time
        if (gameTime <= 0)
        {
            SetIsGameOver(true);
        }
        time -= Time.deltaTime;
        if (time <= 0)
        {
            gameTime--;
            timerCountDownAnimator.SetTrigger(GameStrings.timerAnimation);
            time = 1;
        }
    }

    /// <summary>
    /// Each time the player completes a tile set, it will add the timer by 2
    /// </summary>
    /// <param name="amount">Amount of time to add to startTimer, less time means harder game</param>
    public void AddExtraTime(int amount)
    {
        amountOfTimePlayed = PlayerPrefs.GetInt(GameStrings.amountOfTimePlayed, 0);
        addTimerText.text = "+" + amount;
        addTimeAnimator.SetTrigger(GameStrings.timerAddtionAnimation);
        gameTime += amount;
        PlayerPrefs.SetInt(GameStrings.amountOfTimePlayed, amountOfTimePlayed + amount);
        // Call some animation or Particle effect

    }

    public void AddScore(int amount)
    {
        int score = GetCorrectCombo() <= 0 ? amount : amount * GetCorrectCombo();
        addScoreText.text = "+" + score;
        AudioManager.instance.Play(GameStrings.scoreUpSound);
        addScoreAnimator.SetTrigger(GameStrings.scoreAdditionAnimation);
        playerScore += score;
    }

    /// <summary>
    /// We are setting the player score to be the player highscore if they got a higher score
    /// </summary>
    private void SavePlayerHighscore()
    {
        if (playerScore > GetPlayerHighScore())
        {
            SetPlayerHighScore(playerScore);
            // Play NEW! Text and animation \\
            newHighscoreText.SetActive(true);
        }
    }

    /// <summary>
    /// Updates the text elements
    /// </summary>
    private void UpdateEmements()
    {
        timerText.text = gameTime.ToString("00");
        scoreText.text = playerScore.ToString();
        highscoreText.text = "HS: " + GetPlayerHighScore().ToString();
    }

    public void GameOver()
    {
        if (gameoverScreen.activeInHierarchy == true) return;
        amountOfTimePlayed = PlayerPrefs.GetInt(GameStrings.amountOfTimePlayed, 0);
        correctCombo = 0;
        gameoverScreen.SetActive(true);
        currentScoreText.text = "Score:\n" + playerScore;
        currentHighscoreText.text = "Highscore:\n" + GetPlayerHighScore();
        PlayerPrefs.SetInt(GameStrings.amountOfTimePlayed, amountOfTimePlayed + startTimer);
        // Every 5 games played, play a transitional Ad
        if ((amountOfGames % playAdEvery == 0) && (amountOfGames != 0) && adPlayed == false)
        {
            adPlayed = true;
            Advertisement.Show(adPlacementID);
        }
    }

    /// <summary>
    /// Gets the value of the boolean variable isGameOver
    /// </summary>
    /// <returns>isGameOver</returns>
    public bool GetIsGameOver()
    {
        return isGameOver;
    }

    /// <summary>
    /// Sets the isGameOver Variable to the parameter value when its called
    /// </summary>
    /// <param name="gameOver">The state of the game</param>
    public void SetIsGameOver(bool gameOver)
    {
        isGameOver = gameOver;
    }

    /// <summary>
    /// Gets the value of gameStarted value when its called
    /// </summary>
    /// <returns>Return gameStarted</returns>
    public bool GetGameStarted()
    {
        return gameStarted;
    }

    /// <summary>
    /// Sets the gameStarted value to the parameter didStart when this method is called
    /// </summary>
    /// <param name="didStart">The state of the game</param>
    public void SetGameStarted(bool didStart)
    {
        gameStarted = didStart;
    }

    /// <summary>
    /// Returns the current compo amount
    /// </summary>
    /// <returns>correctCombo variable</returns>
    public int GetCorrectCombo()
    {
        return correctCombo;
    }

    public int GetPlayerHighScore()
    {
        return PlayerPrefs.GetInt(GameStrings.playerHighscore, 0);
    }

    public void SetPlayerHighScore(int playerScore)
    {
        PlayerPrefs.SetInt(GameStrings.playerHighscore, playerScore);
    }

    public bool GetIsGamePaused()
    {
        return isGamePaused;
    }

    public void SetIsGamePaused(bool gameState)
    {
        isGamePaused = gameState;
    }

    /// <summary>
    /// It gives the player current score when its activated
    /// </summary>
    /// <returns>Player score</returns>
    public int GetPlayerScore()
    {
        return playerScore;
    }

    public void SetGameTimer(int extraTimer)
    {
        amountOfTimePlayed = PlayerPrefs.GetInt(GameStrings.amountOfTimePlayed, 0);
        gameTime = extraTimer;
        PlayerPrefs.SetInt(GameStrings.amountOfTimePlayed, amountOfTimePlayed + extraTimer);
    }

    IEnumerator CountDown()
    {
        //Time.timeScale = 0;
        int counter = countdown;
        countdownText.gameObject.SetActive(true);
        playerHighscore.text = "HS: " + GetPlayerHighScore().ToString();
        while (counter >= 0)
        {
            countdownText.text = counter.ToString();
            yield return new WaitForSecondsRealtime(0.5f);
            counter--;
            countDownAnimator.SetTrigger(GameStrings.countDownAnimation);
        }
        tileController.ResetArray();
        tileController.RandomizeArray();
        SetGameStarted(true);
        countdownText.gameObject.SetActive(false);
        //Time.timeScale = 1;
    }

    public void StartTheTimer()
    {
        StartCoroutine(CountDown());
    }

    public void GameOverScreenActiveIs(bool screenStatus)
    {
        gameoverScreen.SetActive(screenStatus);
    }
}
