using UnityEngine;

public class SpriteChanger : MonoBehaviour
{
    [SerializeField] private Sprite [] sprite;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            int rnd = GetRandomSpriteIndex();
            Sprite targetSprite = GetRandomSprite(rnd);
            SetSprite(targetSprite);

           // _spriteRenderer.sprite = sprite[Random.Range(0, sprite.Length)];

        }

        Debug.Log("Sprite");
    }

    private void SetSprite(Sprite targetSprite)
    {
        _spriteRenderer.sprite = targetSprite;
    }

    private Sprite GetRandomSprite(int rnd)
    {
        return sprite[rnd];
    }

    private int GetRandomSpriteIndex()
    {
        return Random.Range(0, sprite.Length);
    }

}
