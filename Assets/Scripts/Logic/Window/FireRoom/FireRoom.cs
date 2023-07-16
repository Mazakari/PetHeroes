using Unity.VisualScripting;
using UnityEngine;

public class FireRoom : MonoBehaviour
{
    public bool InFire {get; private set;}

    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private BoxCollider2D _roomCollider;
    [SerializeField] private Sprite[] _fireSprites;
    [SerializeField] private float _maxFireTimer;
        
    private int _currentFireIndex;
    private float _currentFireTimer = 0;

    private ILevelProgressService _levelProgressService;

    private void OnEnable()
    {
        _levelProgressService = AllServices.Container.Single<ILevelProgressService>();
    }

    private void Start()
    {
        InitFireRoom();
    }

    private void Update()
    {
        FireTimer();
    }

    public void DecreaseCurrentFireIndex()
    {
        _currentFireIndex--;
        _currentFireIndex = Mathf.Clamp(_currentFireIndex, 0, _fireSprites.Length - 1);
        if (_currentFireIndex > 0)
        {
            ResetFireTimer();
        }
        else
        {
            ExtiguishFire();
        }
        ActivateCurrentFire();

    }

    private void FireTimer()
    {
        if(InFire == true)
        {
            _currentFireTimer += Time.deltaTime;
            if (_currentFireTimer >= _maxFireTimer)
            {
                ResetFireTimer();
                FireStatus();

            }
        }
    }

    private void InitFireRoom()
    {
        if (_fireSprites.Length > 0)
        {
            InFire = true;
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

    private void ResetFireTimer()
    {
        _currentFireTimer = 0;
    }
    private void ExtiguishFire()
    {
        InFire = false;
        _roomCollider.enabled = false;
        _levelProgressService.CheckIfAllFireRoomsExtinguished();
    }


}
