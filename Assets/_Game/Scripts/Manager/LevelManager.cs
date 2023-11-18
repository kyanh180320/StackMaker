using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public PlayerParent player;
    public List<Level> listLevels = new List<Level>();
    private Level currentLevel;
    private int levelIndex = 1;
    private void Start()
    {
        LoadLevelIndex();
        LoadLevel(levelIndex);
        DataManager.Instance.GetLevelIndexText(levelIndex);

        GameManager.OnRetryLevel += RetryLevel;
        GameManager.OnNextLevel += NextLevel;

    }
    private void OnDestroy()
    {
        GameManager.OnRetryLevel -= RetryLevel;
        GameManager.OnNextLevel -= NextLevel;
    }



    public void LoadLevel(int levelIndex)
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }
        currentLevel = Instantiate(listLevels[levelIndex - 1]);
    }
    public void NextLevel()
    {
        levelIndex++;
        if (levelIndex > listLevels.Count)
        {
            levelIndex = 1;
            DataManager.Instance.ResetLevel();
        }
        LoadLevel(levelIndex);
        OnInit();
        SaveLevelIndex();
        

    }

    public void RetryLevel()
    {
        LoadLevel(levelIndex);
        OnInit();

    }
    public void OnInit()
    {
        player.gameObject.transform.position = currentLevel.startPosition.position;
        player.OnInit();
    }
    public void SaveLevelIndex()
    {
        PlayerPrefs.SetInt("levelIndex", levelIndex);
    }
    public void LoadLevelIndex()
    {
        levelIndex = PlayerPrefs.GetInt("levelIndex");
    }
}
