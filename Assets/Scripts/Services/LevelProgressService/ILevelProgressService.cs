using System;

public interface ILevelProgressService : IService
{
    int CurrentPlayerLives { get; }
    int PlayerScores { get; }

    event Action OnGameOver;
    event Action OnLevelWin;
    event Action OnTotalScoresChanged;

    void AddScores(int value);
    void CheckIfAllFireRoomsExtinguished();
    void DecreasePlayerLives();
    void InitFireRooms();
}