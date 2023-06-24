using UnityEngine;

public class LoadMainMenuState : IPayloadedState<string>
{
    private readonly GameStateMachine _gameStateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly IGameFactory _gameFactory;
    private readonly LoadingCurtain _curtain;
    private readonly IPersistentProgressService _progressService;
    private readonly ILevelCellsService _cellsService;
    private readonly IYandexService _yandexService;

    public LoadMainMenuState(
        GameStateMachine gameStateMachine, 
        SceneLoader sceneLoader, 
        LoadingCurtain curtain, 
        IGameFactory gameFactory, 
        IPersistentProgressService progressService, 
        ILevelCellsService cellsService,
        IYandexService yandexService)
    {
        _gameStateMachine = gameStateMachine;
        _sceneLoader = sceneLoader;
        _gameFactory = gameFactory;
        _curtain = curtain;
        _gameFactory = gameFactory;
        _progressService = progressService;
        _cellsService = cellsService;
        _yandexService = yandexService;
    }

    public void Enter(string sceneName)
    {
        Debug.Log("LoadMainMenuState");
        _curtain.Show();
        _gameFactory.Cleanup();
        _sceneLoader.Load(sceneName, OnLoaded);
    }

    public void Exit() =>
        _curtain?.Hide();

    private void OnLoaded()
    {
        InitMainMenu();
        InitVolumeControl();
        InitLevelCells();

        InformProgressReaders();
        _gameStateMachine.Enter<MainMenuState>();
    }

    private void InitMainMenu()
    {
        _gameFactory.CreateMainMenulHud();

#if !UNITY_EDITOR
        CheckPlayerAuth();
#endif
    }

    private void InitVolumeControl()
    {
        VolumeControl vc = Object.FindObjectOfType<VolumeControl>();
        if (vc != null) return;

        _gameFactory.CreateVolumeControl();
    }

    private void InitLevelCells() => 
        _cellsService.InitService();

    private void InformProgressReaders()
    {
        foreach (ISavedProgress progressReader in _gameFactory.ProgressReaders)
        {
            progressReader.LoadProgress(_progressService.Progress);
        }
    }

    private void CheckPlayerAuth() =>
       _yandexService.API.CheckAuthorizedStatus();
}
