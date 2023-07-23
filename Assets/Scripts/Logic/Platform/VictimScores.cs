using UnityEngine;

public class VictimScores : MonoBehaviour
{
    [SerializeField] private int _playerLayer;
    [SerializeField] private ShowVictimScore[] _victimScores;

    private int _totalScores = 0;
    private  int _score;

    private ILevelProgressService _progressService;

    private void OnEnable() => 
        _progressService = AllServices.Container.Single<ILevelProgressService>();

    private void OnCollisionEnter2D(Collision2D collision) => 
        CalculateBasketsScores(collision);

    private void CalculateBasketsScores(Collision2D collision)
    {
        if (collision.gameObject.layer == _playerLayer)
        {
            VictimBasketsControl basketsControl = collision.gameObject.GetComponentInChildren<VictimBasketsControl>();
            if (basketsControl != null)
            {
                GetAndShowBasketsScores(basketsControl);
                UpdateTotalScoresInUi();
                DeactivateBaskets(basketsControl);
            }
        }
    }
    private void GetAndShowBasketsScores(VictimBasketsControl basketsControl)
    {
        _totalScores = 0;
        for (int i = 0; i < basketsControl.Basket.Length; i++)
        {
            _score = 0;
            VictimBasket basket = basketsControl.Basket[i];
            if (basket.gameObject.activeSelf)
            {
                if (basket.SavedVictim != null)
                {
                    ShowScoreInUi(basketsControl, i);
                    IncrementTotalScores();
                    Debug.Log($"Scores = {_totalScores}");
                }
            }
        }
    }

    private void ShowScoreInUi(VictimBasketsControl basketsControl, int i)
    {
        _score = basketsControl.Basket[i].SavedVictim.GetScore();
        if (i < _victimScores.Length)
        {
            _victimScores[i].ShowScoreText(_score);
        }
    }
    private void IncrementTotalScores() =>
       _totalScores += _score;

    private void UpdateTotalScoresInUi() =>
       _progressService.AddScores(_totalScores);

    private static void DeactivateBaskets(VictimBasketsControl basketsControl) => 
        basketsControl.DeactivateBaskets();
}
