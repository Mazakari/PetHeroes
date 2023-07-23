using UnityEngine;

public class PlatformCollision : MonoBehaviour
{
    [SerializeField] private int _platform;
    [SerializeField] private Rigidbody2D _platformRigidbody;
    private IDropable _dropable;

    [Space(10)]
    [Header("Audio")]
    [SerializeField] private ItemSound _itemSound;

    private void OnTriggerEnter2D(Collider2D collider) =>
        CollideWithPlatform(collider);

    public void SetDropableReference(IDropable dropable) =>
        _dropable = dropable;

    private void CollideWithPlatform(Collider2D collider)
    {
        if (collider.gameObject.layer == _platform)
        {
            PlayItemSound();
            ActivateDroppableEffect();
            ResetDropabable();
        }
    }

    private void ActivateDroppableEffect() => 
        _dropable.Activate();

    private void PlayItemSound()
    {
        if (_itemSound)
        {
            _itemSound.Play();
        }
    }

    private void ResetDropabable()
    {
        gameObject.SetActive(false);
        _platformRigidbody.isKinematic = true;
        _platformRigidbody.velocity = Vector3.zero;
    }
}
