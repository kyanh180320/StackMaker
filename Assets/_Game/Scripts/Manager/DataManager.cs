using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    // Start is called before the first frame update
    public TextMeshProUGUI textScore;
    public TextMeshProUGUI textLevel;
    private int score = 0;
    private int levelIndex = 1;

    private void Start()
    {


        GameManager.OnRetryLevel += ResetScore;
        GameManager.OnNextLevel += IncreaseLevel;
        GameManager.OnNextLevel += ResetScore;

    }
    private void OnDestroy()
    {
        GameManager.OnNextLevel -= IncreaseLevel;
        GameManager.OnRetryLevel -= ResetScore;
        GameManager.OnNextLevel -= ResetScore;
    }

    public void IncreaseScore()
    {
        score++;
        textScore.text = score.ToString();
    }
    public void ResetScore()
    {
        score = 0;
        textScore.text = score.ToString();
    }
    public void IncreaseLevel()
    {
        levelIndex++;
        UpdateTextLevel();
    }
    public void UpdateTextLevel()
    {
        textLevel.text = "Level " + levelIndex.ToString();
    }

    public void ResetLevel()
    {
        levelIndex = 1;
        UpdateTextLevel();
    }
    public void GetLevelIndexText(int value)
    {
        this.levelIndex = value;
        UpdateTextLevel();
    }





}
