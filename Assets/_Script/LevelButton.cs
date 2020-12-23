using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public Levels levelAssigned;
    private int levelIndex;
    private bool isSelectedLevel;

    // Start is called before the first frame update
    void Start()
    {
        SetupLevelButton();
    }

    // Update is called once per frame
    void Update()
    {
        if (isSelectedLevel == true)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            isSelectedLevel = PlayerPrefs.GetInt(GameStrings.selectedLevel, 1) == levelIndex ? true : false; // Find something better to implement this
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
            isSelectedLevel = PlayerPrefs.GetInt(GameStrings.selectedLevel, 1) == levelIndex ? true : false;
        }
    }

    private void SetupLevelButton()
    {
        GetComponent<Button>().image.sprite = levelAssigned.levelImage;
        if (levelAssigned.isLocked == true) GetComponent<Button>().interactable = false;
        levelIndex = levelAssigned.levelIndex;
        isSelectedLevel = PlayerPrefs.GetInt(GameStrings.selectedLevel, 1) == levelIndex ? true : false;
    }

    public void ThisLevelIsSelected()
    {
        PlayerPrefs.SetInt(GameStrings.selectedLevel, levelIndex);
        AudioManager.instance.Play(GameStrings.buttonsClickedSound);
    }
}
