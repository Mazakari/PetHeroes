using TMPro;
using UnityEngine;

public class PlayerLivesCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _livesScoreText;
    private ILevelProgressService _levelProgressService;

    private void OnEnable()
    {
        _levelProgressService = AllServices.Container.Single<ILevelProgressService>();
        
        _levelProgressService.OnPlayerLivesChanged += UpdateLivesCounter;
        UpdateLivesCounter();
    }

    private void OnDisable()
    {
        _levelProgressService.OnPlayerLivesChanged -= UpdateLivesCounter;
    }

    private void UpdateLivesCounter()
    {
        _livesScoreText.text = _levelProgressService.CurrentPlayerLives.ToString();
    }
}
