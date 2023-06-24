using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;

    [SerializeField] private Sprite _backgroundSprite;
    [SerializeField] private Vector2 _parallaxEffectMultiplier;

    [SerializeField] private bool _infiniteHorizontal = true;
    [SerializeField] private bool _infiniteVertical = true;

    private Vector3 _lastCameraPosition;
    private Vector3 _deltaMovement;

    private Texture2D _spriteTexture;
    private float _textureUnitSizeX;
    private float _textureUnitSizeY;


    private void Start()
    {
        SetTextureUnitSize();
        InitLastCameraPosition();
    }

    void LateUpdate()
    {
        FollowCamera();
        MoveBackgroundSeamles();
    }

    private void SetTextureUnitSize()
    {
        _spriteTexture = _backgroundSprite.texture;
        _textureUnitSizeX = _spriteTexture.width / _backgroundSprite.pixelsPerUnit;
        _textureUnitSizeY = _spriteTexture.height / _backgroundSprite.pixelsPerUnit;
    }

    private void InitLastCameraPosition() => 
        _lastCameraPosition = _cameraTransform.position;

    private void FollowCamera()
    {
        _deltaMovement = _cameraTransform.position - _lastCameraPosition;
        transform.position += new Vector3(_deltaMovement.x * _parallaxEffectMultiplier.x, _deltaMovement.y * _parallaxEffectMultiplier.y);

        _lastCameraPosition = _cameraTransform.position;
    }

    private void MoveBackgroundSeamles()
    {
        if (_infiniteHorizontal)
        {
            MoveSeamlesByX();
        }

        if (_infiniteVertical)
        {
            MoveSeamlesByY();
        }
    }
    private void MoveSeamlesByX()
    {
        float offsetPosX = (_cameraTransform.position.x - transform.position.x) % _textureUnitSizeX;
        if (Mathf.Abs(_cameraTransform.position.x - transform.position.x) >= _textureUnitSizeX)
        {
            transform.position = new Vector3(_cameraTransform.position.x + offsetPosX, transform.position.y);
        }
    }
    private void MoveSeamlesByY()
    {
        float offsetPosY = (_cameraTransform.position.y - transform.position.y) % _textureUnitSizeY;
        if (Mathf.Abs(_cameraTransform.position.y - transform.position.y) >= _textureUnitSizeY)
        {
            transform.position = new Vector3(transform.position.x, _cameraTransform.position.y + offsetPosY);
        }
    }
}
