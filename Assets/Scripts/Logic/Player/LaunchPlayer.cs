using PetHeroes.Inputs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchPlayer : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform _platform;

    [SerializeField] private  float Force = 800f;
    [SerializeField] private  float OffSetX = 100f;

    private bool _isActive = false;
    private void Start()
    {
        rb.isKinematic = true;
        _isActive = false;
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Window"))
            Debug.Log("Saved");
        if (col.gameObject.CompareTag("Loose"))
            Debug.Log("Looser");
    }
       
    void Update()
    {
        if (_isActive == true)
        {
            return;
        }

        Vector2 playerPos = new Vector2(_platform.position.x, transform.position.y);
        transform.position = playerPos;

        if (Input.GetButtonDown(GlobalStringVars.PUSH))
        {
            PlayerActive();
            _isActive = true;
        }
       
    }

    private void PlayerActive()
    {
        rb.isKinematic = false; 
        rb.AddForce(new Vector2(OffSetX, Force));
    }
}
