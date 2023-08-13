using UnityEngine;

public class Victim : MonoBehaviour
{
    [SerializeField] private int _score = 10;
       
    public int GetScore() =>
        _score;
}