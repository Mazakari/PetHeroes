using System;

public interface ILevelProgressService : IService
{
    int CurrentPlayerLives { get; }

    event Action OnGameOver;
    event Action OnLevelWin;

    void CheckIfAllFireRoomsExtinguished();
    void DecreasePlayerLives();
    void InitFireRooms();
}