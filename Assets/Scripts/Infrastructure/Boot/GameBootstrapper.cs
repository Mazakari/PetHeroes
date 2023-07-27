using UnityEngine;

public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
{
    [SerializeField] private LoadingCurtain _curtain;
    [SerializeField] private YandexAPI _yandexApi;

    private Game _game;

    private void Awake()
    {
        //LoadingCurtain curtain = Instantiate(_curtainPrefab);
        //YandexAPI api = Instantiate(_yandexApiPrefab);

        _game = new Game(this, _curtain, _yandexApi);
        _game.StateMachine.Enter<BootstrapState>();

        DontDestroyOnLoad(this);
    }
}
