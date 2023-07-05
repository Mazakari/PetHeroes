using UnityEngine;

public class VictimsRoom : MonoBehaviour
{
    [SerializeField] private GameObject[] _victimsPrefabs;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private Victim[] _victims;
    private int _currentVictimIndex = 0;
    private int _previoustVictimIndex = 0;
    private Victim _currentVictim;

    private void OnEnable() => 
        InitRoom();

    public Victim SaveCurrentVictim()
    {
        ActivateNextVictim();
        return _currentVictim;
    }

    private void InitRoom()
    {
        _currentVictimIndex = 0;
        _previoustVictimIndex = _currentVictimIndex;

        InitVictimsArray();
        SpawnVictimsPrefabs();
        ActivateNextVictim();
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
        GameObject victim = Instantiate(_victimsPrefabs[i], transform);
        victim.SetActive(false);
        return victim;
    }
    private void AddVictim(GameObject victim, int i) => 
        _victims[i] = victim.GetComponent<Victim>();
    private void ActivateNextVictim()
    {
        DeactivatePreviousVictim();
        ActivateCurrentVictim();

        IncrementPreviousAndCurrentIndex();
    }
    private void DeactivatePreviousVictim()
    {
        Victim previousVictim = _victims[_previoustVictimIndex];
        previousVictim.gameObject.SetActive(false);
    }

    private void ActivateCurrentVictim()
    {
        _currentVictim = _victims[_currentVictimIndex];

        _currentVictim.gameObject.SetActive(true);
        SetRoomSprite(_currentVictim.GetSprite());
    }
    private void IncrementPreviousAndCurrentIndex()
    {
        _previoustVictimIndex = _currentVictimIndex;
        _currentVictimIndex++;

        _currentVictimIndex = Mathf.Clamp(_currentVictimIndex, 0, _victims.Length - 1);
    }

    private void SetRoomSprite(Sprite sprite) =>
       _spriteRenderer.sprite = sprite;
}
