using UnityEngine;

public class LoadProgressState : IState
{
    private readonly GameStateMachine _gameStateMachine;
    private readonly IPersistentProgressService _progressService;
    private readonly ISaveLoadService _saveLoadService;
    private readonly IYandexService _yandexService;
    private readonly ISkinsService _skinService;


    public LoadProgressState(
        GameStateMachine gameStateMachine, 
        IPersistentProgressService progressService, 
        ISaveLoadService saveLoadService, 
        IYandexService yandexService, 
        ISkinsService skinService)
    {
        _gameStateMachine = gameStateMachine;
        _progressService = progressService;
        _saveLoadService = saveLoadService;
        _yandexService = yandexService;
        _skinService = skinService;
    }

    public void Enter()
    {
        Debug.Log("LoadProgressState");
#if UNITY_EDITOR
        LoadProgressOrInitNew(true);
        _gameStateMachine.Enter<LoadMainMenuState, string>(Constants.MAIN_MENU_SCENE_NAME);
#endif

#if!UNITY_EDITOR
        _yandexService.API.OnAuthorizedStatusResponse += LoadPlayerProgress;
        _yandexService.API.OnYandexProgressCopied += LoadProgressFromCloud;

        CheckPlayerAuth();
#endif
    }

    public void Exit()
    {
#if !UNITY_EDITOR
        _yandexService.API.OnAuthorizedStatusResponse -= LoadPlayerProgress;
        _yandexService.API.OnYandexProgressCopied -= LoadProgressFromCloud;
      
#endif
    }

    private void CheckPlayerAuth() =>
       _yandexService.API.CheckAuthorizedStatus();

    private void LoadPlayerProgress()
    {
        if (_yandexService.API.PlayerLoggedIn)
        {
            // Copy save to _yandexService.API.PlayerProgress and await for OnYandexProgressCopied callback
            _yandexService.API.LoadFromYandex();
        }
        else
        {
            LoadProgressOrInitNew(true);
            _gameStateMachine.Enter<LoadMainMenuState, string>(Constants.MAIN_MENU_SCENE_NAME);
        }
    }

    private void LoadProgressFromCloud()
    {
        Debug.Log("LoadProgressState.LoadProgressFromCloud from Yandex");

        LoadProgressOrInitNew(false);
        _gameStateMachine.Enter<LoadMainMenuState, string>(Constants.MAIN_MENU_SCENE_NAME);
    }
    private void LoadProgressOrInitNew(bool local) =>
        _progressService.Progress = _saveLoadService.LoadProgress(local) ?? NewProgress();

    private PlayerProgress NewProgress()
    {
        Debug.Log("Cloud Progress is null. Create new progress");
        return new(
            initialMoney: Constants.NEW_PROGRESS_PLAYER_MONEY_AMOUNT, 
            initialLevel: Constants.NEW_PROGRESS_FIRST_LEVEL_SCENE_NAME, 
            _skinService.DefaultSkinPrefab);
    }
}
