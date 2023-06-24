using System;
using UnityEngine;

public class GameLoopState : IState
{
    private readonly GameStateMachine _gameStateMachine;
    private readonly SceneLoader _sceneLoader;

    private string _currentLevelName;
    private int _currentLevelNumber;
    private string _nextLevelName;

    public static event Action<string> OnNextLevelNameSet;
    public static event Action<int> OnCurrentLevelSet;

    public GameLoopState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
    {
        _gameStateMachine = gameStateMachine;
        _sceneLoader = sceneLoader;
    }

    public void Enter()
    {
        Debug.Log("GameLoopState");

        SetLevelNames();

        GameplayCanvas.OnMainMenuButton += LoadMainMenu;
        GameplayCanvas.OnNextLevel += LoadNextLevel;

        GameplayCanvas.OnRestartLevel += RestartLevel;
        DeathTrigger.OnDeadZoneEnter += RestartLevel;
        EnemyCollision.OnEnemyCollision += RestartLevel;
    }
    public void Exit()
    {
        GameplayCanvas.OnMainMenuButton -= LoadMainMenu;
        GameplayCanvas.OnNextLevel -= LoadNextLevel;

        GameplayCanvas.OnRestartLevel -= RestartLevel;
        DeathTrigger.OnDeadZoneEnter -= RestartLevel;
        EnemyCollision.OnEnemyCollision -= RestartLevel;
    }

    private void SetLevelNames()
    {
        _currentLevelName = _sceneLoader.GetCurrentLevelName();
        _currentLevelNumber = _sceneLoader.GetCurrentLevelNumber();
        OnCurrentLevelSet?.Invoke(_currentLevelNumber);

        _nextLevelName = _sceneLoader.GetNextLevelName();
        OnNextLevelNameSet?.Invoke(_nextLevelName);
    }

    private void LoadMainMenu() =>
        _gameStateMachine.Enter<LoadMainMenuState, string>(Constants.MAIN_MENU_SCENE_NAME);

    private void RestartLevel() =>
        _gameStateMachine.Enter<LoadLevelState, string>(_currentLevelName);

    private void LoadNextLevel() => 
        _gameStateMachine.Enter<LoadLevelState, string>(_nextLevelName);
}
