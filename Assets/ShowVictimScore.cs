using UnityEngine;
using TMPro;


public class ShowVictimScore : MonoBehaviour
{
    private const float DISPLAY_TIME = 2f;
    [SerializeField] private TMP_Text _scoreText;
    private Color _scoreColor;
    private float _fadeState = 0.01f;
    private void OnEnable()
    {
        _scoreColor = _scoreText.color;
        _scoreText.enabled = false;
    }

    public void ShowScoreText(int score)
    {

        _scoreText.text = score.ToString();
        SetColorAlpha(1f);
        _scoreText.enabled = true;
        Invoke("DisableText", DISPLAY_TIME);
    }

    private void DisableText()
    {
        SetColorAlpha(0);
    }
    private void SetColorAlpha(float a)
    {
        a = Mathf.Clamp01(a);
        _scoreColor.a = a;
        _scoreText.color = _scoreColor;
    }
    private void FadeInColor()
    {
        for (int i = 0; i < 1; )
        {

        }
    }
}
