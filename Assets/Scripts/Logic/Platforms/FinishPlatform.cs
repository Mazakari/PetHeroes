using System;
using UnityEngine;

public class FinishPlatform : MonoBehaviour
{
    [SerializeField] private int _playerLayer;

    [Space(10)]
    [Header("Audio")]
    [SerializeField] private ItemSound _itemSound;

    public static event Action OnLevelFinish;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == _playerLayer)
        {
            if (PlayerAbovePlatform(collision.gameObject.transform))
            {
                Debug.Log("Finish");

                PlayItemSound();


                // Callback for LevelState
                OnLevelFinish?.Invoke();
            }

        }
    }

    private bool PlayerAbovePlatform(Transform player)
    {
        float playerYPosition = player.position.y;
        float platformYPosition = transform.position.y;

        return playerYPosition > platformYPosition;
    }

    private void PlayItemSound()
    {
        if (_itemSound)
        {
            _itemSound.Play();
        }
    }
}
