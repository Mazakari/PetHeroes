using UnityEngine;

public class FireRoom : MonoBehaviour
{
    public bool InFire {get; private set;}

    [SerializeField] private BoxCollider2D _roomCollider;
    [SerializeField] private GameObject[] _firePrefabs;
    [SerializeField] private float _maxFireTimer;
        
    private int _currentFireIndex;
    private int _previousFireIndex;

    private float _currentFireTimer = 0;

    private ILevelProgressService _levelProgressService;

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
        }
        else
        {
            ExtiguishFire();
            // TO DO Add sound on fire extinguished
        }

        ActivateCurrentFire();

        // TO DO Play fire growth sound
        PlayFireGrowthSound();
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
        Debug.Log($"Initial current index = {_currentFireIndex}");
    }
    private void ActivateCurrentFire()
    {
        //Debug.Log($"Previous index = {_previousFireIndex}");
        //Debug.Log($"Current index = {_currentFireIndex}");
        _firePrefabs[_previousFireIndex].SetActive(false);
        _firePrefabs[_currentFireIndex].SetActive(true);
    }

    private void PlayFireGrowthSound()
    {
        if (_currentFireIndex > 0)
        {
            // TO DO Play fire growth sound
        }
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
    private void ResetFireTimer() =>
       _currentFireTimer = 0;

    private void ExtiguishFire()
    {
        InFire = false;
        _roomCollider.enabled = false;
        _levelProgressService.CheckIfAllFireRoomsExtinguished();
    }
   
    private void FireStatus()
    {
        _previousFireIndex = _currentFireIndex;
        _currentFireIndex++;
        _currentFireIndex = Mathf.Clamp(_currentFireIndex, 0, _firePrefabs.Length - 1);
        ActivateCurrentFire();
    }
}
