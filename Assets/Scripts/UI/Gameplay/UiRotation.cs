using System.Collections;
using UnityEngine;

public class UiRotation : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    private Vector3 _rotationAxis = Vector3.forward;
    private void OnEnable() => 
        StartCoroutine(Rotate());

    private void OnDisable() => 
        StopCoroutine(Rotate());

    private IEnumerator Rotate()
    {
        while (true)
        {
            transform.Rotate(_rotationAxis, _speed);
            yield return null;
        }
    }
}
