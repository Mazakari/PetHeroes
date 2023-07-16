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

    public LoadLevelState(
        GameStateMachine gameStateMachine, 
        SceneLoader sceneLoader, 
        LoadingCurtain curtain, 
        IGameFactory gameFactory, 
        IPersistentProgressService progressService, 
        ILevelCellsService levelCellsService, 
        ILevelProgressService levelProgressService)
    {
        _gameStateMachine = gameStateMachine;
        _sceneLoader = sceneLoader;
        _curtain = curtain;
        _gameFactory = gameFactory;
        _progressService = progressService;
        _levelCellsService = levelCellsService;
        _levelProgressService = levelProgressService;
    }

    public void Enter(string sceneName)
    {
        Debug.Log("LoadLevelState");
        _curtain.Show();
        _gameFactory.Cleanup();
        _sceneLoader.Load(sceneName, OnLoaded);
    }

    public void Exit()
    {
        _curtain.Hide();
    }

    private void OnLoaded()
    {
        InitGameWorld();
        InformProgressReaders();
        _gameStateMachine.Enter<GameLoopState>();
    }

    private void InformProgressReaders()
    {
        foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
        {
            progressReader.LoadProgress(_progressService.Progress);
        }
    }

    private void InitGameWorld()
    {
        GameObject spawnPos = GameObject.FindGameObjectWithTag(Constants.PLAYER_SPAWN_POINT_TAG);
        GameObject player = _gameFactory.CreatePlayer(spawnPos);

        SetPlatformReference(player);
        SetSpawnPointReferrence(spawnPos, player);

        _levelCellsService.SetCurrentCell();
        _gameFactory.CreateLevelHud();

        _levelProgressService.InitFireRooms();
    }

    private void SetPlatformReference(GameObject player)
    {
        PlatformInput platform = Object.FindObjectOfType<PlatformInput>();
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
}