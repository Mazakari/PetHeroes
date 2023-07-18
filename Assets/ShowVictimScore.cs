using UnityEngine;
using TMPro;


public class ShowVictimScore : MonoBehaviour
{
    private const float DISPLAY_TIME = 2f;
    [SerializeField] private TMP_Text _scoreText;

    private void OnEnable()
    {
        _scoreText.enabled = false;
    }

    public void ShowScoreText(int score)
    {
       _scoreText.text = score.ToString();
       _scoreText.enabled = true;
       Invoke("DisableText", DISPLAY_TIME);
    }

    private void DisableText()
    {
        _scoreText.enabled = false;
    }

}
