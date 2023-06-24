using Cinemachine;
using UnityEngine;

public class LoadLevelState : IPayloadedState<string>
{
    private readonly GameStateMachine _gameStateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingCurtain _curtain;
    private readonly IGameFactory _gameFactory;
    private readonly IPersistentProgressService _progressService;
    private readonly ILevelCellsService _levelCellsService;

    public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain curtain, IGameFactory gameFactory, IPersistentProgressService progressService, ILevelCellsService levelCellsService)
    {
        _gameStateMachine = gameStateMachine;
        _sceneLoader = sceneLoader;
        _curtain = curtain;
        _gameFactory = gameFactory;
        _progressService = progressService;
        _levelCellsService = levelCellsService;
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

        _levelCellsService.SetCurrentCell();

        InitTreasureChest();

        _gameFactory.CreateLevelHud();

        Transform followTransform = player.GetComponent<Transform>();
        CinemachineCameraFollow(followTransform);
    }

    private void CinemachineCameraFollow(Transform player)
    {
        CinemachineVirtualCamera cam = Object.FindObjectOfType<CinemachineVirtualCamera>();
        cam.Follow = player;
    }

    private static void InitTreasureChest()
    {
        TreasureChest chest = Object.FindObjectOfType<TreasureChest>();
        if (chest)
        {
            chest.InitChest();
        }
    }
}