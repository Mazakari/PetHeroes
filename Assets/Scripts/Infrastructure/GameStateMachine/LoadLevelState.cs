using UnityEngine;

public class LoadLevelState : IPayloadedState<string>
{
    private readonly GameStateMachine _gameStateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingCurtain _curtain;
    private readonly IGameFactory _gameFactory;
    private readonly IPersistentProgressService _progressService;
    private readonly ILevelCellsService _levelCellsService;
    private readonly ILevelProgressService _levelProgressService;
    private readonly IDropService _dropService;

    public LoadLevelState(
        GameStateMachine gameStateMachine, 
        SceneLoader sceneLoader, 
        LoadingCurtain curtain, 
        IGameFactory gameFactory, 
        IPersistentProgressService progressService, 
        ILevelCellsService levelCellsService, 
        ILevelProgressService levelProgressService,
        IDropService dropService)
    {
        _gameStateMachine = gameStateMachine;
        _sceneLoader = sceneLoader;
        _curtain = curtain;
        _gameFactory = gameFactory;
        _progressService = progressService;
        _levelCellsService = levelCellsService;
        _levelProgressService = levelProgressService;
        _dropService = dropService;
    }

    public void Enter(string sceneName)
    {
        Debug.Log("LoadLevelState");
        _curtain.Show();
        _gameFactory.Cleanup();
        _sceneLoader.Load(sceneName, OnLoaded);
    }

    public void Exit() => 
        _curtain.Hide();

    private void OnLoaded()
    {
        InitServices();
        InitGameWorld();
        ResetTotalLevelScores();
        InformProgressReaders();

        _gameStateMachine.Enter<GameLoopState>();
    }

    private void InitServices()
    {
        _levelCellsService.SetCurrentCell();
        _levelProgressService.InitFireRooms();
        _dropService.InitDropablesPools();
    }

    private void InitGameWorld()
    {
        GameObject spawnPos = GameObject.FindGameObjectWithTag(Constants.PLAYER_SPAWN_POINT_TAG);
        GameObject player = _gameFactory.CreatePlayer(spawnPos);
        PlatformInput platform = Object.FindObjectOfType<PlatformInput>();
        _gameFactory.CreateLevelHud();

        SetPlatformReferrence(platform, player);
        SetSpawnPointReferrence(spawnPos, player);
    }
    private void SetPlatformReferrence(PlatformInput platform, GameObject player)
    {
        if (platform != null)
        {
            if (player.TryGetComponent(out LaunchPlayer launcher))
            {
                launcher.SetPlatformReference(platform.transform);
            }
        }
    }
    private void SetSpawnPointReferrence(GameObject spawnPos, GameObject player)
    {
        if (player.TryGetComponent(out PlayerRespawn respawn))
        {
            respawn.SetRespawnPointReferrence(spawnPos.transform);
        }
    }

    private void ResetTotalLevelScores() =>
       _levelProgressService.ResetScores();
   

    private void InformProgressReaders()
    {
        foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
        {
            progressReader.LoadProgress(_progressService.Progress);
        }
    }
}