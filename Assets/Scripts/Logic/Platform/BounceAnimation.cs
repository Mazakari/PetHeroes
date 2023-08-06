using System.Collections;
using UnityEngine;

public class BounceAnimation : MonoBehaviour
{
    [SerializeField] private float _spriteSwitchDelay = 0.5f;

    [Space(10)]
    [Header("Components References")]
    [SerializeField] private SpriteRenderer _bodyRenderer;

    [Space(10)]
    [Header("Sprites")]
    [SerializeField] private Sprite _idleSprite;
    [SerializeField] private Sprite _bendDownSprite;
    [SerializeField] private Sprite _bendUpSprite;


    private void OnEnable() => 
        InitBodySprite();

    public void PlayAnimation()
    {
        StopCoroutine(SwitchSprites());
        StartCoroutine(SwitchSprites());
    }

    private void InitBodySprite() =>
       SetBodySprite(_idleSprite);

    private void SetBodySprite(Sprite sprite)
    {
        if (_bodyRenderer == null)
        {
            Debug.LogError("Platform Sprite Renderer Component not set");
            return;
        }

        if (sprite == null)
        {
            Debug.LogError("Body sprite not set");
            return;
        }

        _bodyRenderer.sprite = sprite;
    }

    private IEnumerator SwitchSprites()
    {
        SetBodySprite(_bendDownSprite);
        yield return new WaitForSeconds(_spriteSwitchDelay);

        SetBodySprite(_bendUpSprite);
        yield return new WaitForSeconds(_spriteSwitchDelay);

        SetBodySprite(_idleSprite);

    }
}
