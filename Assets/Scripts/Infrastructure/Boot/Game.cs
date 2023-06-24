public class Game
{
    public GameStateMachine StateMachine;

    public Game(ICoroutineRunner coroutineRunner, LoadingCurtain curtain, YandexAPI yandexAPI)
    {
        StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), curtain, yandexAPI, AllServices.Container);
    }
}
