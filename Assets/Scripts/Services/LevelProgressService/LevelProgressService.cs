using System;
using UnityEngine;

public class LevelProgressService : ILevelProgressService
{
    private int _maxPlayerLives;
    public int CurrentPlayerLives { get; private set; }

    public event Action OnGameOver;
    public LevelProgressService()
    {
        _maxPlayerLives = Constants.MAX_PLAYER_LIVES;
        ResetLives();
    }

    private void ResetLives()
    {
        CurrentPlayerLives = _maxPlayerLives;
    }

    public void DecreasePlayerLives()
    {
        CurrentPlayerLives--;
        CurrentPlayerLives = Mathf.Clamp(CurrentPlayerLives, 0, _maxPlayerLives);

        if (CurrentPlayerLives <= 0 ) 
        {
            Debug.Log("Game Over");
            ResetLives();
            // Send callback for GameplayState
            OnGameOver?.Invoke();
        }
    }
}
