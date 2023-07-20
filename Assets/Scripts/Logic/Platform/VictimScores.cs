using UnityEngine;

public class VictimScores : MonoBehaviour
{
    [SerializeField] private int _playerLayer;
    [SerializeField] private ShowVictimScore[] _victimScores;
    private TotalScoreCount _totalScoreCount;

    private int _totalScores = 0;
    private  int _score;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == _playerLayer)
        {
            VictimBasketsControl basketsControl = collision.gameObject.GetComponentInChildren<VictimBasketsControl>();
            if (basketsControl != null)
            {
                for (int i = 0; i < basketsControl.Basket.Length; i++)
                {
                    if (basketsControl.Basket[i].gameObject.activeSelf)
                    {
                        _score = basketsControl.Basket[i].SavedVictim.GetScore();
                        if( i < _victimScores.Length)
                        {
                         _victimScores[i].ShowScoreText(_score);
                        }
                        _totalScores += basketsControl.Basket[i].SavedVictim.GetScore();
                        Debug.Log($"Scores = {_totalScores}");
                    }
                }
                _totalScoreCount.ShowTotalScoreText(_totalScores);
                basketsControl.DeactivateBaskets();
            }
        }

    }

    public void SetTotalScoreReferrence(TotalScoreCount totalScoreCount) => 
        _totalScoreCount = totalScoreCount;

}
