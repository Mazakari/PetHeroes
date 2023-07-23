using TMPro;
using UnityEngine;

public class TotalScoreCount : MonoBehaviour
{
    [SerializeField] private TMP_Text _totalScoreText;
    private ILevelProgressService _levelProgressService;

    private void OnEnable()
    {
        _levelProgressService = AllServices.Container.Single<ILevelProgressService>();

        _levelProgressService.OnTotalScoresChanged += UpdateScoreCounter;
    }

    private void OnDisable() => 
        _levelProgressService.OnTotalScoresChanged -= UpdateScoreCounter;

    private void UpdateScoreCounter() => 
        _totalScoreText.text = _levelProgressService.PlayerScores.ToString();
}
