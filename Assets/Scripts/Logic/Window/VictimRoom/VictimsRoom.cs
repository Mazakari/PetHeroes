using UnityEngine;

public class VictimsRoom : MonoBehaviour
{
    [SerializeField] private GameObject[] _victimsPrefabs;
    [SerializeField] private Transform _victimsSpawnPoint;

    [SerializeField] private BoxCollider2D _roomCollider;

    private Victim[] _victims;
    private int _currentVictimIndex = 0;
    private Victim _currentVictim;

    private void OnEnable() => 
        InitRoom();

    public Victim GetCurrentVictim() =>
        _currentVictim;
    public void ActivateNextVictim()
    {
        _currentVictimIndex++;

        if (_currentVictimIndex < _victims.Length)
        {
            ActivateCurrentVictim();
        }
        else
        {
            _roomCollider.enabled = false;
        }
    }

    //private void DeactivateCurrentVictim() => 
    //    _victims[_currentVictimIndex].gameObject.SetActive(false);

    private void InitRoom()
    {
        _currentVictimIndex = 0;

        InitVictimsArray();
        SpawnVictimsPrefabs();
        ActivateCurrentVictim();
    }

    private void InitVictimsArray()
    {
        int victimsCount = _victimsPrefabs.Length;
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
        _currentVictim = _victims[_currentVictimIndex];

        _currentVictim.gameObject.SetActive(true);
    }
}
