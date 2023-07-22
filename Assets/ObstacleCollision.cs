using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCollision : MonoBehaviour
{
    [SerializeField] private int _HP;
    [SerializeField] private int _playerLayer;
    [SerializeField] private ItemSound _sound;
    [SerializeField] private BoxCollider2D _collider;

    private void OnEnable()
    {
     _collider.enabled = true;

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        DecreaseHPandPlaySound(collision);

    }

    private void DecreaseHPandPlaySound(Collision2D collision)
    {
        if (collision.gameObject.layer == _playerLayer)
        {
            if (_HP > 0)
            {
                _HP--;
            }
            else
            {
                _collider.enabled = false;
            }
        }

        PlayCollisionSound();
    }

    private void PlayCollisionSound()
    {
        _sound.Play();
    }
}
