using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public Levels[] allLevels;
    public List<Levels> unlockedLevels = new List<Levels>();

    void Start()
    {
        GetAllUnlockedLevels();
    }

    public void GetAllUnlockedLevels()
    {
        int currentHighscore = PlayerPrefs.GetInt(GameStrings.playerHighscore, 0);
        for (int i = 0; i < allLevels.Length; i++)
        {
            allLevels[i].isLocked = false;
            PlayerPrefs.SetInt(allLevels[i].levelName, 1);
            unlockedLevels.Add(allLevels[i]);
            // // Check the levels thats unlocked through highscores
            // if (currentHighscore >= allLevels[i].requiredHighscoreToUnlock)
            // {
            //     allLevels[i].isLocked = false;
            //     PlayerPrefs.SetInt(allLevels[i].levelName, 1);
            //     unlockedLevels.Add(allLevels[i]);
            // }
            // // Write other checks underhere, such as how many ads watched
        }
    }

    private bool CheckUnlockedLevelsAt(int levelIndex)
    {
        for (int i = 0; i < unlockedLevels.Count; i++)
        {
            if (unlockedLevels[i].levelIndex == levelIndex)
            {
                return true;
            }
        }
        return false;
    }
}
