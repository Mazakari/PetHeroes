using UnityEngine;

public class VictimScores : MonoBehaviour
{
    private int _totalScores = 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Constants.PLAYER_TAG))
        {
            VictimBasketsControl basketsControl = collision.gameObject.GetComponentInChildren<VictimBasketsControl>();
            if (basketsControl != null)
            {
                for (int i = 0; i < basketsControl.Basket.Length; i++)
                {
                    if (basketsControl.Basket[i].gameObject.activeSelf)
                    {
                        _totalScores += basketsControl.Basket[i].SavedVictim.GetScore();
                        Debug.Log($"Scores = {_totalScores}");
                    }
                }
            }
        }
    }
}
