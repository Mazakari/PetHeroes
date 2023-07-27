using TMPro;
using UnityEngine;

public class TotalScoreCount : MonoBehaviour, ISavedProgress
{
    [SerializeField] private TMP_Text _totalScoreText;

    private ILevelProgressService _levelProgressService;
    private IMetaResourcesService _metaResourcesService;
    private IYandexService _yandexService;

    private void OnEnable()
    {
        _levelProgressService = AllServices.Container.Single<ILevelProgressService>();
        _metaResourcesService = AllServices.Container.Single<IMetaResourcesService>();
        _yandexService = AllServices.Container.Single<IYandexService>();

        _levelProgressService.OnTotalScoresChanged += UpdateScoreCounter;
    }

    private void OnDisable() => 
        _levelProgressService.OnTotalScoresChanged -= UpdateScoreCounter;

    private void UpdateScoreCounter() => 
        _totalScoreText.text = _levelProgressService.PlayerScores.ToString();


    public void UpdateProgress(PlayerProgress progress)
    {
        int Levelscores = _levelProgressService.PlayerScores;
        _metaResourcesService.PlayerMoney += Levelscores;
        progress.gameData.playerMoney += Levelscores;

#if !UNITY_EDITOR
        // Save yandex leaderboard
        _yandexService.API.SaveYandexLeaderboard(_metaResourcesService.PlayerMoney);
#endif
    }

    public void LoadProgress(PlayerProgress progress)
    {
       
    }
}
