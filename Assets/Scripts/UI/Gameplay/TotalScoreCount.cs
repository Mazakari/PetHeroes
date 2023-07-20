using TMPro;
using UnityEngine;

public class TotalScoreCount : MonoBehaviour
{
    [SerializeField] private TMP_Text _totalScoreText;

    public void ShowTotalScoreText(int score)
    {
        _totalScoreText.text = score.ToString();
       
    }
}
