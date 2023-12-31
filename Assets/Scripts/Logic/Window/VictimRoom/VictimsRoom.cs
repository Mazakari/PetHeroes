using UnityEngine;

public class VictimsRoom : MonoBehaviour
{
    [SerializeField] private GameObject[] _victimsPrefabs;
    [SerializeField] private Transform _victimsSpawnPoint;

    [SerializeField] private BoxCollider2D _roomCollider;

    private Victim[] _victims;
    private int _victimsToSave = 0;
    private int _currentVictimIndex = 0;
    private Victim _currentVictim;

    private void OnEnable() => 
        InitRoom();

    public Victim GetCurrentVictim() =>
        _currentVictim;

    public void ActivateNextVictim()
    {
        GenerateRandomVictimIndex();

        if (_victimsToSave > 0)
        {
            ActivateCurrentVictim();
        }
        else
        {
            _roomCollider.enabled = false;
        }
    }

    private void GenerateRandomVictimIndex()
    {
        int rnd = Random.Range(0, _victims.Length);
        while (_victims[rnd].gameObject.activeSelf) 
        {
            rnd = Random.Range(0, _victims.Length);
        }

        _currentVictimIndex = rnd;
    }

    private void InitRoom()
    {
        InitVictimsArray();
        SpawnVictimsPrefabs();

        GenerateRandomVictimIndex();
        ActivateCurrentVictim();

    }

    private void InitVictimsArray()
    {
        int victimsCount = _victimsPrefabs.Length;
        _victimsToSave = victimsCount;
        _victims = new Victim[victimsCount];
    }
    private void SpawnVictimsPrefabs()
    {
        GameObject victim;

        for (int i = 0; i < _victimsPrefabs.Length; i++)
        {
            victim = SpawnVictim(i);
            AddVictim(victim, i);
        }
    }
    private GameObject SpawnVictim(int i)
    {
        GameObject victim = Instantiate(_victimsPrefabs[i], _victimsSpawnPoint);
        victim.SetActive(false);
        return victim;
    }
    private void AddVictim(GameObject victim, int i) => 
        _victims[i] = victim.GetComponent<Victim>();
   
    private void ActivateCurrentVictim()
    {
        _victimsToSave--;
        _currentVictim = _victims[_currentVictimIndex];

        _currentVictim.gameObject.SetActive(true);
    }
}
