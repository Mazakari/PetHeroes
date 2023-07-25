using UnityEngine;

public class LevelCompletePopup : MonoBehaviour
{
    [Space(10)]
    [Header("Audio")]
    [SerializeField] private ItemSound _itemSound;

    private ISaveLoadService _saveLoadService;
    private IYandexService _yandexService;


    private void OnEnable()
    {
        _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
        _yandexService = AllServices.Container.Single<IYandexService>();

#if !UNITY_EDITOR
        ShowInterstitialAds();
#endif
        // SaveProgress
        _saveLoadService.SaveProgress();

        _itemSound.Play();
    }

    public void RestartLevel() => 
        GameplayCanvas.OnRestartLevel?.Invoke();

    public void LoadNextLevel() => 
        GameplayCanvas.OnNextLevel?.Invoke();

    private void ShowInterstitialAds() =>
       _yandexService.API.ShowYandexInterstitial();
}
