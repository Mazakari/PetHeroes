using UnityEngine;

public class VictimBasket : MonoBehaviour
{
    [SerializeField] private SpriteRenderer SpriteRenderer;
    public Victim SavedVictim { get; private set; }
    
    public void PlaceVictimInBasket(Victim victim)
    {
        SavedVictim = victim;
        SpriteRenderer.sprite = SavedVictim.GetSprite();
    }

    public void ClearSavedVictim() => 
        SavedVictim = null;
}
