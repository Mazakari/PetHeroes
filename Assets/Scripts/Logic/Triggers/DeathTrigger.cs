using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    [SerializeField] private int _playerLayer;


    [Space(10)]
    [Header("Audio")]
    [SerializeField] private ItemSound _itemSound;

    

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == _playerLayer)
        {
            PlayItemSound();
            if (collider.TryGetComponent(out PlayerRespawn player))
            {
                player.Respawn();
            }
            
           
        }
    }

    private void PlayItemSound()
    {
        if (_itemSound)
        {
            _itemSound.Play();
        }
    }
}
