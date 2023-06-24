using System;
using UnityEngine;

public class TimeService : ITimeService
{
    public bool IsGamePaused { get; private set; }

    public event Action OnGamePause;
    public event Action OnGameResume;

    public void PauseGame()
    {
        if (!IsGamePaused)
        {
            Time.timeScale = 0;
            IsGamePaused = true;
            OnGamePause?.Invoke();
        }
    }

    public void ResumeGame()
    {
        if (IsGamePaused)
        {
            Time.timeScale = 1;
            IsGamePaused = false;
            OnGameResume?.Invoke();
        }
    }
}
