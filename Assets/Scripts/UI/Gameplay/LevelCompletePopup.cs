using TMPro;
using UnityEngine;

public class LevelCompletePopup : MonoBehaviour
{
    //[SerializeField] private GameObject _rewardedBlock;
    [SerializeField] private TMP_Text _totalScoresCounter;

    [Space(10)]
    [Header("Audio")]
    [SerializeField] private ItemSound _itemSound;

    private ISaveLoadService _saveLoadService;
    private ILevelProgressService _levelProgressService;

    private void OnEnable()
    {
        _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
        _levelProgressService = AllServices.Container.Single<ILevelProgressService>();

        // TO DO replace with myTargetRewarded
        //_yandexService.API.OnRewardedVideoWatched += AddRewardedBonus;

        UpdateTotalScoreCounter();

        // TO DO replace with myTargetRewarded
        //ShowRewardedButton();

#if !UNITY_EDITOR
        //ShowInterstitialAds();
#endif
        // SaveProgress
        SaveProgress();

        PlayLevelCompleteSound();
    }

    private void OnDisable()
    {
        // TO DO replace with myTargetRewarded
        //_yandexService.API.OnRewardedVideoWatched -= AddRewardedBonus;
    }

    public void RestartLevel() =>
       GameplayCanvas.OnRestartLevel?.Invoke();

    public void LoadNextLevel() =>
        GameplayCanvas.OnNextLevel?.Invoke();

    // TO DO replace with myTargetRewarded
    private void ShowRewardedButton()
    {

        bool showRewardedButton = _levelProgressService.PlayerScores > 0;
        //_rewardedBlock.SetActive(showRewardedButton);
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

    // TO DO replace with myTarget
    //private void ShowInterstitialAds() =>
    //   _yandexService.API.ShowYandexInterstitial();
}
