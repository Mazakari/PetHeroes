using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _maxMoveSpeed = 10;
    [SerializeField] private float _smoothTime = 0.3f;
    [SerializeField] private bool _smooth = false;
    private Vector2 _currentVelocity;

    void Update() => 
        ReadMouseInputAndMovePlayer();

    private void ReadMouseInputAndMovePlayer()
    {
        Vector2 mousePos = GetMouseWorldPosition();

        if (_smooth)
        {
            SmoothFollowMouseCursor(mousePos);
        }
        else
        {
            StickToMouseCursor(mousePos);
        }
    }

    private Vector2 GetMouseWorldPosition()
    {
        Vector2 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        return mousePos;
    }

    private void StickToMouseCursor(Vector2 mousePos) => 
        transform.position = new Vector2(mousePos.x, transform.position.y);
    private void SmoothFollowMouseCursor(Vector2 mousePosition)
    {
        Vector2 newPos = Vector2.SmoothDamp(transform.position, mousePosition, ref _currentVelocity, _smoothTime, _maxMoveSpeed);
        newPos.y = transform.position.y;

        transform.position = newPos;
    }

    private void ClampInsideView()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos = Camera.main.ViewportToWorldPoint(pos);

        transform.position = pos;
    }
}
