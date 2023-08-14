using UnityEngine;

public class FireRoomCollision : MonoBehaviour
{
    [SerializeField] private int _playerLayer;
    [SerializeField] private FireRoom _room;
    [SerializeField] private RoomDrop _drop;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == _playerLayer)
        {
            DecreaseFireLevelAndDropItem();
        }
    }

    private void DecreaseFireLevelAndDropItem()
    {
        _room.DecreaseCurrentFireIndex();
        _drop.DropItem();
    }
}

