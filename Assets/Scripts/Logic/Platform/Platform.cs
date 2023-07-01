using PetHeroes.Inputs;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField]
    [Range (10f, 100f)]

    public float speed = 10;

       
    void Update()
    {
        MovePlatform();
        ClampHorizontalMovement();
    }

    private void MovePlatform()
    {
        transform.Translate(speed * Vector3.right * Input.GetAxisRaw(GlobalStringVars.HORIZONTAL_AXIS) * Time.deltaTime);
       
    }

    private void ClampHorizontalMovement()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -16f, 16f), transform.position.y, transform.position.z);
    }
}
