using UnityEngine;

public class FireRoom : MonoBehaviour
{
    public bool InFire {get; private set;}
    
    [SerializeField] private BoxCollider2D _roomCollider;
    [SerializeField] private GameObject[] _firePrefabs;
    [SerializeField] private float _maxFireTimer;
    private bool _fireMaxed = false;

    private int _currentFireIndex;
    private int _previousFireIndex;

    private float _currentFireTimer = 0;

    private ILevelProgressService _levelProgressService;

    [Space(10)]
    [Header("Effects")]
    [SerializeField] private FireSound _sound;
    [SerializeField] private ParticlesEffect _particlesEffect;

    private void OnEnable() => 
        _levelProgressService = AllServices.Container.Single<ILevelProgressService>();

    private void Start() => 
        InitFireRoom();

    private void Update() => 
        FireTimer();

    public void DecreaseCurrentFireIndex()
    {
        _previousFireIndex = _currentFireIndex;
        _currentFireIndex--;
        _currentFireIndex = Mathf.Clamp(_currentFireIndex, 0, _firePrefabs.Length - 1);
        if (_currentFireIndex > 0)
        {
            ResetFireTimer();
            _fireMaxed = false;
        }
        else
        {
            ExtiguishFire();
        }

        ActivateCurrentFire();
        PlayFireDownSound();
    }

    private void InitFireRoom()
    {
        if (_firePrefabs.Length > 0)
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
        int prefabsCount = _firePrefabs.Length;
        int indexRnd = Random.Range(1, prefabsCount);
        _currentFireIndex = indexRnd;
        _previousFireIndex = _currentFireIndex;
    }
    private void ActivateCurrentFire()
    {
        _firePrefabs[_previousFireIndex].SetActive(false);
        _firePrefabs[_currentFireIndex].SetActive(true);
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

                PlayFireGrowthSound();
            }
        }
    }

    private void ResetFireTimer() =>
       _currentFireTimer = 0;

    private void ExtiguishFire()
    {
        InFire = false;
        _roomCollider.enabled = false;
        _levelProgressService.CheckIfAllFireRoomsExtinguished();
        _particlesEffect.Play();
    }
   
    private void FireStatus()
    {
        _previousFireIndex = _currentFireIndex;
        _currentFireIndex++;
        _currentFireIndex = Mathf.Clamp(_currentFireIndex, 0, _firePrefabs.Length - 1);
        CheckIfMaximumFire();

        ActivateCurrentFire();
    }

    private void CheckIfMaximumFire()
    {
        if (_previousFireIndex == _currentFireIndex && _fireMaxed == false)
        {
            _fireMaxed = true;
        }
    }

    private void PlayFireGrowthSound()
    {
        if (_fireMaxed == false)
        {
            _sound.PlayFireGrowSound();
        }
    }

    private void PlayFireDownSound() =>
        _sound.PlayFireDownSound();
}
