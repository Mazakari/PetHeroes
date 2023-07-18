
using TMPro;
using UnityEngine;

public class Victim : MonoBehaviour
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private int _score = 10;

    private TMP_Text _scoreText; 
       
    public Sprite GetSprite() =>
        _sprite;

    public int GetScore()
    {
        return _score;
    }

   
}