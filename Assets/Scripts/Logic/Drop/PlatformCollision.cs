using System.Collections;
using UnityEngine;

public class PlatformCollision : MonoBehaviour
{
    [SerializeField] private int _platform;
    [SerializeField] private int _deathTrigger;
    [SerializeField] private Rigidbody2D _platformRigidbody;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Collider2D _collider;

    private IDropableItem _dropable;
    private IDropService _dropService;

    [Space(10)]
    [Header("Audio")]
    [SerializeField] private ItemSound _itemSound;

    private void OnEnable() => 
        _dropService = AllServices.Container.Single<IDropService>();

    private void OnTriggerEnter2D(Collider2D collider) =>
        CollideWithObject(collider);

    public void SetDropableReference(IDropableItem dropable) =>
        _dropable = dropable;
    public void ActivateDropabable()
    {
        transform.parent.gameObject.SetActive(true);
        _platformRigidbody.isKinematic = false;
    }

    public void ResetDropabable()
    {
        transform.parent.gameObject.SetActive(false);
        _platformRigidbody.isKinematic = true;
        _platformRigidbody.velocity = Vector3.zero;
        _platformRigidbody.gameObject.transform.position = Vector3.zero;
        _spriteRenderer.enabled = true;
        _platformRigidbody.isKinematic = false;
        _collider.enabled = true;
    }

    private void CollideWithObject(Collider2D collider)
    {
        if (collider.gameObject.layer == _platform)
        {
            StartCoroutine(UseDroppableAndPoolIt());
            return;
        }
        if (collider.gameObject.layer == _deathTrigger)
        {
            PoolDropable();
            return;
        }
    }
    private IEnumerator UseDroppableAndPoolIt()
    {
        PlayItemSound();
        ActivateDroppableEffect();
        DeactivateDropabable();
        yield return new WaitForSeconds(1f);
        PoolDropable();

    }
    private void PlayItemSound()
    {
        if (_itemSound)
        {
            _itemSound.Play();
        }
    }

    private void ActivateDroppableEffect() => 
        _dropable.Use();
   
    private void DeactivateDropabable()
    {
        _spriteRenderer.enabled = false;
        _platformRigidbody.isKinematic = true;
        _collider.enabled = false;
    }
    private void PoolDropable() =>
       _dropService.PoolDroppable(transform.parent.gameObject);
}
