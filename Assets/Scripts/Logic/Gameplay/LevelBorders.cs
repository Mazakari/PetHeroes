using UnityEngine;

public class LevelBorders : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private Camera _camera;
    private float _camHeight;
    private float _camWidth;

    [Space(10)]
    [Header("Borders")]
    [SerializeField] private Transform _topBorder;
    [SerializeField] private Transform _leftBorder;
    [SerializeField] private Transform _rightBorder;

    private void OnEnable() => 
        SetBorders();

    private void SetBorders()
    {
        GetCameraSize();
        CalculateAndSetBordersPositions();
    }

    private void CalculateAndSetBordersPositions()
    {
        CalculateTopBorder();

        float xOffset = _camWidth / 2f;
        CalculateAndSetLeftBorder(xOffset);
        CalculateAndSetRightBorder(xOffset);
    }
    private void CalculateAndSetLeftBorder(float xOffset)
    {
        Vector2 borderPosition = Vector2.zero;
        borderPosition.x -= xOffset;
        _leftBorder.position = borderPosition;
    }

    private void CalculateAndSetRightBorder(float xOffset)
    {
        Vector2 borderPosition = Vector2.zero;

        borderPosition = Vector2.zero;
        borderPosition.x += xOffset;
        _rightBorder.position = borderPosition;
    }

    private void CalculateTopBorder()
    {
        Vector2 borderPosition = Vector2.zero;

        borderPosition.y += _camHeight / 2f;
        _topBorder.position = borderPosition;
    }

    private void GetCameraSize()
    {
        _camHeight = 2f * _camera.orthographicSize;
        _camWidth = _camHeight * _camera.aspect;
    }
}
