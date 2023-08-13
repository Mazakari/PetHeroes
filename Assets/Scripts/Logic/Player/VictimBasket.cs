using UnityEngine;

public class VictimBasket : MonoBehaviour
{
    [SerializeField] private Transform basketPoint;
    public Victim SavedVictim { get; private set; }
    
    public void PlaceVictimInBasket(Victim victim)
    {
        SavedVictim = victim;
        victim.transform.SetParent(basketPoint, false);
    }

    public void ClearSavedVictim()
    {
        if (SavedVictim != null)
        {
            SavedVictim.gameObject.SetActive(false);
            SavedVictim = null;
        }
    }
}
