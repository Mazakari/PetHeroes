using System;
using UnityEngine;

public class MainMenuState : IState
{
    private readonly GameStateMachine _gameStateMachine;
    private readonly IPersistentProgressService _progressService;
    private readonly ISaveLoadService _saveLoadService;
    private readonly ISkinsService _skinService;

    public MainMenuState(
        GameStateMachine gameStateMachine, 
        IPersistentProgressService progressService, 
        ISaveLoadService saveLoadService, 
        ISkinsService skinService)
    {
        _gameStateMachine = gameStateMachine;
        _progressService = progressService;
        _saveLoadService = saveLoadService;
        _skinService = skinService;
    }

    public void Enter()
    {
        Debug.Log("MainMenuState");
        LevelCell.OnLevelCellPress += StartGame;
        ContinueGame.OnContinueGamePress += StartGame;
        MainMenuCanvas.OnShopButtonPress += LoadShop;
    }

    public void Exit()
    {
        LevelCell.OnLevelCellPress -= StartGame;
        ContinueGame.OnContinueGamePress -= StartGame;
        MainMenuCanvas.OnShopButtonPress -= LoadShop;
    }

    private void StartGame(string levelName) =>
      _gameStateMachine.Enter<LoadLevelState, string>(levelName);
   
    private void LoadShop() =>
       _gameStateMachine.Enter<LoadShopState, string>(Constants.SHOP_SCENE_NAME);
}
