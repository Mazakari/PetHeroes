using UnityEngine;
using UnityEngine.UI;

public class LevelCompletePopup : MonoBehaviour
{
    [SerializeField] private GameObject _artifactLockedBody;

    [Header("Buttons")]
    [Space(10)]
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private Button _restartButton;

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
        _nextLevelButton.onClick.AddListener(LoadNextLevel);
        _restartButton.onClick.AddListener(RestartLevel);

        // SaveProgress
        _saveLoadService.SaveProgress();

        _itemSound.Play();
    }

    private void OnDisable()
    {
        _nextLevelButton.onClick.RemoveAllListeners();
        _restartButton.onClick.RemoveAllListeners();
    }

    private void RestartLevel() => 
        GameplayCanvas.OnRestartLevel?.Invoke();

    private void LoadNextLevel() => 
        GameplayCanvas.OnNextLevel?.Invoke();

    public void ShowArtifact(bool locked)
    {
        if (!locked)
        {
            _artifactLockedBody.SetActive(false);
        }
    }

    private void ShowInterstitialAds() =>
       _yandexService.API.ShowYandexInterstitial();
}
