using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject finishLevelUI;

    private void Start()
    {
        GameManager.OnNextLevel += CloseFinishLevelUI;
        GameManager.OnRetryLevel += CloseFinishLevelUI;
        GameManager.OnWin += OpenFinishLevelUI;
    }
    private void OnDestroy()
    {
        GameManager.OnWin -= OpenFinishLevelUI;
        GameManager.OnRetryLevel -= CloseFinishLevelUI;
        GameManager.OnNextLevel -= CloseFinishLevelUI;
    }



    public void OpenFinishLevelUI()
    {
        finishLevelUI.SetActive(true);
    }
    public void CloseFinishLevelUI()
    {
        finishLevelUI.SetActive(false);
    }

    public void NextLevelButton()
    {
        GameManager.Instance.NextLevel();

    }
    public void RetryLevelButton()
    {
        GameManager.Instance.RetryLevel();
    }
}
