using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "New Level", menuName = "Level")]
public class Levels : ScriptableObject
{
    public Sprite levelImage;
    public string levelName;
    public int levelIndex;
    public bool isLocked = true;
    public int requiredHighscoreToUnlock = 0;
    public int currentAdWatched = 0;
    public int adToWatchToUnlock = 0;
}
