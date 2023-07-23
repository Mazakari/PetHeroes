
using TMPro;
using UnityEngine;

public class Victim : MonoBehaviour
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private int _score = 10;
       
    public Sprite GetSprite() =>
        _sprite;

    public int GetScore() =>
        _score;
}