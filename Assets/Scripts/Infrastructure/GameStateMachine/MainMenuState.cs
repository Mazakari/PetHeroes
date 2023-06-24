using System;
using UnityEngine;

public class MainMenuState : IState
{
    private readonly GameStateMachine _gameStateMachine;
    private readonly IPersistentProgressService _progressService;
    private readonly ISaveLoadService _saveLoadService;
    private readonly IYandexService _yandexService;

    public static event Action<PlayerProgress> OnAuthorizationPlayerProgressSynced;

    public MainMenuState(GameStateMachine gameStateMachine, IPersistentProgressService progressService, ISaveLoadService saveLoadService, IYandexService yandexService)
    {
        _gameStateMachine = gameStateMachine;
        _progressService = progressService;
        _saveLoadService = saveLoadService;
        _yandexService = yandexService;
    }

    public void Enter()
    {
        Debug.Log("MainMenuState");
        LevelCell.OnLevelCellPress += StartGame;

#if UNITY_WEBGL
        _yandexService.API.OnYandexProgressCopied += LoadProgressFromCloud;
#endif
    }

    public void Exit()
    {
        LevelCell.OnLevelCellPress -= StartGame;

#if UNITY_WEBGL
        _yandexService.API.OnYandexProgressCopied -= LoadProgressFromCloud;
#endif
    }

    private void StartGame(string levelName) =>
      _gameStateMachine.Enter<LoadLevelState, string>(levelName);

    private void LoadProgressFromCloud()
    {
        Debug.Log("MainMenuState.LoadProgressFromCloud from Yandex");

        LoadProgressOrInitNew(false);

       
    }
    private void LoadProgressOrInitNew(bool local)
    {
        SaveLocalProgressToCloudIfCloudNull(local);
        LoadCloudProgress(local);

        OnAuthorizationPlayerProgressSynced?.Invoke(_progressService.Progress);
    }

    private void SaveLocalProgressToCloudIfCloudNull(bool local)
    {
        PlayerProgress cloudProgress = _saveLoadService.LoadProgress(local);

        if (cloudProgress == null && _progressService.Progress != null)
        {
            string progress = _progressService.Progress.ToJson();
            _yandexService.API.SaveToYandex(progress);
        }
    }

    private void LoadCloudProgress(bool local) =>
       _progressService.Progress = _saveLoadService.LoadProgress(local) ?? NewProgress();

    private PlayerProgress NewProgress()
    {
        Debug.Log("Cloud Progress is null. Create new progress");
        return new(initialLevel: Constants.NEW_PROGRESS_FIRST_LEVEL_SCENE_NAME);
    }

}
