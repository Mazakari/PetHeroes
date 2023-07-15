using Unity.VisualScripting;
using UnityEngine;

public class FireRoom : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private BoxCollider2D _roomCollider;
    [SerializeField] private Sprite[] _fireSprites;
    [SerializeField] private float _maxFireTimer;
        
    private int _currentFireIndex;
    private float _currentFireTimer = 0;

    private void Start()
    {
        InitFireRoom();
    }

    private void Update()
    {
        _currentFireTimer += Time.deltaTime;
        if (_currentFireTimer >= _maxFireTimer)
        {
            _currentFireTimer = 0;
            FireStatus();

        }
    }
    private void InitFireRoom()
    {
        if (_fireSprites.Length > 0)
        {
            InitFireArray();
            ActivateCurrentFire();
        }
        else
        {
            Debug.LogError("_fireSprites is empty");
        }
    }
    private void InitFireArray()
    {
        int fireSpriteNumber = _fireSprites.Length;
        int indexRnd = Random.Range(1, fireSpriteNumber);
        _currentFireIndex = indexRnd;
        Debug.Log(indexRnd);
    }
    private void FireStatus()
    {
        _currentFireIndex++;
        _currentFireIndex = Mathf.Clamp(_currentFireIndex, 0, _fireSprites.Length - 1);
        ActivateCurrentFire();
    }
    private void ActivateCurrentFire()
    {
        _spriteRenderer.sprite = _fireSprites[_currentFireIndex];

    }

   

        
}
