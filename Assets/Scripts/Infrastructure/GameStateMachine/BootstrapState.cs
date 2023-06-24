using UnityEngine;

public class BootstrapState : IState
{
    private readonly GameStateMachine _gameStateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly YandexAPI _yandexApi;
    private readonly AllServices _services;

    public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, YandexAPI yandexAPI, AllServices services)
    {
        _gameStateMachine = gameStateMachine;
        _sceneLoader = sceneLoader;
        _yandexApi = yandexAPI;
        _services = services;

        RegisterServices();
    }

    public void Enter()
    {
        SetFpsTarget();

        // TO DO Unity bug with GetSceneByBuildIndex() Init scene names manualy
        _sceneLoader.GetBuildNamesFromBuildSettings();

        Debug.Log("BootstrapState");
        _sceneLoader.Load(Constants.INITIAL_SCENE_NAME, onLoaded: EnterLoadLevel);
    }

    private void EnterLoadLevel() =>
        _gameStateMachine.Enter<LoadProgressState>();

    public void Exit() {}

    private void RegisterServices()
    {
        //_services.RegisterSingle<IInputService>(new InputService());
        _services.RegisterSingle<ITimeService>(new TimeService());
        _services.RegisterSingle<IYandexService>(new YandexService(_yandexApi, _services.Single<ITimeService>()));
        _services.RegisterSingle<IAssets>(new AssetProvider());
        _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
        _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssets>()));
        _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(_services.Single<IPersistentProgressService>(), _services.Single<IGameFactory>(), _services.Single<IYandexService>()));
        _services.RegisterSingle<ILevelCellsService>(new LevelCellsService(_services.Single<IGameFactory>(), _sceneLoader));
        _services.RegisterSingle<ILanguageService>(new LanguageService(_services.Single<IYandexService>()));
    }

    // System Settings
    private void SetFpsTarget() =>
        Application.targetFrameRate = 120;
}
