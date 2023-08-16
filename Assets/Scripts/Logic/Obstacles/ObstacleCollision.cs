using UnityEngine;

public class ObstacleCollision : MonoBehaviour
{
    [SerializeField]private int _playerLayer = 6;
    [SerializeField] private ItemSound _sound;

    private void OnCollisionEnter2D(Collision2D collision) =>
        TriggerSound(collision);

    private void TriggerSound(Collision2D collision)
    {
        if (collision.gameObject.layer == _playerLayer)
        {
            PlayCollisionSound();
        }
    }

    private void PlayCollisionSound()
    {
        try
        {
            _sound.Play();
        }
        catch
        {

            Debug.LogError("ItemSound Component reference is null");
        }
        
    }
}
