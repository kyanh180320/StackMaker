using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public static Action OnWin;
    public static Action OnRetryLevel;
    public static Action OnNextLevel;

    public void WinGame()
    {
        OnWin?.Invoke();
    }
    public void NextLevel()
    {
        OnNextLevel?.Invoke();
    }
    public void RetryLevel()
    {
        OnRetryLevel?.Invoke();
    }

}

