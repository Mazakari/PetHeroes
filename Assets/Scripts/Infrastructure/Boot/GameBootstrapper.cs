using UnityEngine;

public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
{
    [SerializeField] private LoadingCurtain _curtainPrefab;

    private Game _game;

    private void Awake()
    {
        LoadingCurtain curtain = Instantiate(_curtainPrefab);

        _game = new Game(this, curtain);
        _game.StateMachine.Enter<BootstrapState>();

        DontDestroyOnLoad(this);
    }
}
