using UnityEngine;

public class SwitchHealthSprites : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private Sprite[] _healthSprites;

    private int _currentSpriteIndex = 0;

    private void OnEnable() => 
        InitFirstSprite();

    public void SwitchHealthSprite()
    {
        _currentSpriteIndex++;
        _currentSpriteIndex = Mathf.Clamp(_currentSpriteIndex, 0, _healthSprites.Length);

        SetNewSprite(_currentSpriteIndex);
    }

    public void DisableSpriteRenderer() => 
        _renderer.enabled = false;

    private void SetNewSprite(int spriteIndex) =>
        _renderer.sprite = _healthSprites[spriteIndex];

    private void InitFirstSprite() => 
        SetNewSprite(_currentSpriteIndex);
}
