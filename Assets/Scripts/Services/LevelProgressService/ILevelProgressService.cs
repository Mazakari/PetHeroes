
using System;

public interface ILevelProgressService : IService
{
    int CurrentPlayerLives { get; }

    event Action OnGameOver;

    void DecreasePlayerLives();
}