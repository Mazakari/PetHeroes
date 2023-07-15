using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    [SerializeField] private PlatformInput _input;
    [SerializeField] private float _speed = 10;

    private void Update() => 
        MovePlatform();

    public void MovePlatform() =>
       transform.Translate(_speed * Time.deltaTime * new Vector2(_input.MoveInputX, 0));
}
