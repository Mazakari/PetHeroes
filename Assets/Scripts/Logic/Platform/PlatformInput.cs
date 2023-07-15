using UnityEngine;

public class PlatformInput : MonoBehaviour
{
    [SerializeField]
    [Range(10f, 100f)]
    private float _speed = 10;

    public void MovePlatform() =>
       transform.Translate(_speed * Input.GetAxisRaw(GlobalStringVars.HORIZONTAL_AXIS) * Time.deltaTime * Vector3.right);

}
