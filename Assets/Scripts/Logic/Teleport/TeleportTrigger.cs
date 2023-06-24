using UnityEngine;

public class TeleportTrigger : MonoBehaviour
{
    [SerializeField] private Teleport _teleport;
    [SerializeField] private int _playerLayer;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == _playerLayer)
        {
            _teleport.TeleportPlayer(collider.transform);
        }
    }
}
