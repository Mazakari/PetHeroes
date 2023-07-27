using UnityEngine;

public class GameRunner : MonoBehaviour
{
    [SerializeField] private GameBootstrapper _bootstrapperPrefab;
    [SerializeField] private YandexAPI _yandexApi;

    private void Awake() => 
        InstantiateBootstrapper();

    private void InstantiateBootstrapper()
    {
        GameBootstrapper bootstrapper = FindAnyObjectByType<GameBootstrapper>();
        if (bootstrapper == null)
        {
            Instantiate(_bootstrapperPrefab);
        }
    }
}
