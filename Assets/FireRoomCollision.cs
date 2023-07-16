using UnityEngine;

public class FireRoomCollision : MonoBehaviour
{
    [SerializeField] private int _playerLayer;
    [SerializeField] private FireRoom _room;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == _playerLayer)
        {
            _room.DecreaseCurrentFireIndex();

        }
    }

    

}

