using UnityEngine;

public class VictimBasket : MonoBehaviour
{
    [SerializeField] private Transform basketPoint;
    public Victim SavedVictim { get; private set; }

    private Transform _victimParentTransform;

    public void PlaceVictimInBasket(Victim victim)
    {
        SavedVictim = victim;
        _victimParentTransform = victim.transform.parent;

        victim.transform.SetParent(basketPoint, false);
    }

    public void ClearSavedVictim()
    {
        if (SavedVictim != null)
        {
            SavedVictim.gameObject.SetActive(false);
            SavedVictim.transform.SetParent(_victimParentTransform, false);

            SavedVictim = null;
        }
    }
}
