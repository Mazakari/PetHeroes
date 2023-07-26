using UnityEngine;

public class YandexRewarded : MonoBehaviour
{
    private IYandexService _yandexService;

    private void OnEnable() => 
        _yandexService = AllServices.Container.Single<IYandexService>();

    public void ShowRewarded()
    {
        _yandexService.API.ShowYandexRewarded();
        gameObject.SetActive(false);
    }
}
