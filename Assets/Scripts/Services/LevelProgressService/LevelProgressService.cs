using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelProgressService : ILevelProgressService
{
    private int _maxPlayerLives;
    public int CurrentPlayerLives { get; private set; }

    private int _playerScores = 0;
    public int PlayerScores => _playerScores;

    private List<FireRoom> _fireRooms;

    public event Action OnGameOver;
    public event Action OnLevelWin;
    public event Action OnTotalScoresChanged;

    public LevelProgressService()
    {
        _maxPlayerLives = Constants.MAX_PLAYER_LIVES;
        ResetLives();
        _fireRooms = new List<FireRoom>();
    }
   
    public void InitFireRooms()
    {
        _fireRooms.Clear();
        _fireRooms = GameObject.FindObjectsOfType<FireRoom>().ToList();
    }

    public void CheckIfAllFireRoomsExtinguished()
    {
        for (int i = 0; i < _fireRooms.Count; i++)
        {
            if (_fireRooms[i].InFire)
            {
                return;
            }
        }

        OnLevelWin?.Invoke();
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

    public void AddScores(int value)
    {
        _playerScores += value;
        OnTotalScoresChanged?.Invoke();
    }

    private void ResetLives() =>
       CurrentPlayerLives = _maxPlayerLives;
}
