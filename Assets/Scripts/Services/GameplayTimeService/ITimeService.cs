using System;

public interface ITimeService : IService
{
    bool IsGamePaused { get; }

    event Action OnGamePause;
    event Action OnGameResume;

    void PauseGame();
    void ResumeGame();
}