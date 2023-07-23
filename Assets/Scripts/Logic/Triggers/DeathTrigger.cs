using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    [SerializeField] private int _playerLayer;

    [Space(10)]
    [Header("Audio")]
    [SerializeField] private ItemSound _itemSound;

    private void OnTriggerEnter2D(Collider2D collider) => 
        CollideWithPlayer(collider);

    private void CollideWithPlayer(Collider2D collider)
    {
        if (collider.gameObject.layer == _playerLayer)
        {
            PlayItemSound();
            RespawnPlayer(collider);
        }
    }

    private void PlayItemSound()
    {
        if (_itemSound)
        {
            _itemSound.Play();
        }
    }

    private void RespawnPlayer(Collider2D collider)
    {
        if (collider.TryGetComponent(out PlayerRespawn player))
        {
            player.Respawn();
        }
    }
}
