using UnityEngine;
using TMPro;
using System.Collections;

public class ShowVictimScore : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    private Color _scoreColor;
    private float _fadeStep = 0.01f;

    private void OnEnable()
    {
        _scoreColor = _scoreText.color;
        _scoreText.enabled = true;
    }

    public void ShowScoreText(int score)
    {
        _scoreText.text = score.ToString();
        _scoreText.enabled = true;
        StartCoroutine(FadeOut());
    }
    
    private void SetColorAlpha(float a)
    {
        a = Mathf.Clamp01(a);
        _scoreColor.a = a;
        _scoreText.color = _scoreColor;
    }

    private IEnumerator FadeOut()
    {
        float alpha = 1;
        while (alpha > 0)
        {
          SetColorAlpha(alpha);
          alpha -= _fadeStep;
          yield return new WaitForSeconds(_fadeStep);
        }
    }
}
