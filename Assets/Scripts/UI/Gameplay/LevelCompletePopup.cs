using TMPro;
using UnityEngine;

public class LevelCompletePopup : MonoBehaviour
{
    [SerializeField] private GameObject _yandexRewardedBlock;
    [SerializeField] private TMP_Text _totalScoresCounter;

    [Space(10)]
    [Header("Audio")]
    [SerializeField] private ItemSound _itemSound;

    private ISaveLoadService _saveLoadService;
    private IYandexService _yandexService;
    private ILevelProgressService _levelProgressService;

    private void OnEnable()
    {
        _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
        _yandexService = AllServices.Container.Single<IYandexService>();
        _levelProgressService = AllServices.Container.Single<ILevelProgressService>();

        _yandexService.API.OnRewardedVideoWatched += AddRewardedBonus;

        UpdateTotalScoreCounter();

        ShowYandexRewardedButton();
#if !UNITY_EDITOR
        ShowInterstitialAds();
#endif
        // SaveProgress
        SaveProgress();

        PlayLevelCompleteSound();
    }

    private void OnDisable() => 
        _yandexService.API.OnRewardedVideoWatched -= AddRewardedBonus;
  
    public void RestartLevel() =>
       GameplayCanvas.OnRestartLevel?.Invoke();

    public void LoadNextLevel() =>
        GameplayCanvas.OnNextLevel?.Invoke();

    private void ShowYandexRewardedButton()
    {
        bool showRewardedButton = _levelProgressService.PlayerScores > 0;
        _yandexRewardedBlock.SetActive(showRewardedButton);
    }

    private void AddRewardedBonus()
    {
        int levelScores = _levelProgressService.PlayerScores;
        int bonusScores = _levelProgressService.PlayerScores * 2;
        int bonusToAdd = Mathf.Abs(bonusScores - levelScores);

        _totalScoresCounter.text = bonusScores.ToString();

        _levelProgressService.AddBonusScores(bonusToAdd);

        SaveProgress();
    }

    private void UpdateTotalScoreCounter() =>
        _totalScoresCounter.text = _levelProgressService.PlayerScores.ToString();

    private void SaveProgress() =>
       _saveLoadService.SaveProgress();
    
    private void PlayLevelCompleteSound() =>
          _itemSound.Play();

    private void ShowInterstitialAds() =>
       _yandexService.API.ShowYandexInterstitial();
}
