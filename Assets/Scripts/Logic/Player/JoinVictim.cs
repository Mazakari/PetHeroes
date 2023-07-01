using UnityEngine;

public class JoinVictim : MonoBehaviour
{
    [SerializeField] private SpriteRenderer SpriteRenderer;
    
    public void SetVictimSprite(Sprite victimSprite)
    {
        SpriteRenderer.sprite = victimSprite;
    }

   
}
