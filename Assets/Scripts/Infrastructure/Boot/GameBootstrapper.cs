using UnityEngine;

public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
{
    [SerializeField] private LoadingCurtain _curtainPrefab;
    [SerializeField] private YandexAPI _yandexApiPrefab;

    private Game _game;

    private void Awake()
    {
        LoadingCurtain curtain = Instantiate(_curtainPrefab);
        YandexAPI api = Instantiate(_yandexApiPrefab);

        _game = new Game(this, curtain, api);
        _game.StateMachine.Enter<BootstrapState>();

        DontDestroyOnLoad(this);
    }
}
