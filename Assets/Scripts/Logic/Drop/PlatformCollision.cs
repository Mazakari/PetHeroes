using UnityEngine;

public class PlatformCollision : MonoBehaviour
{
    [SerializeField] private int _platform;
    [SerializeField] private int _deathTrigger;
    [SerializeField] private Rigidbody2D _platformRigidbody;

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
    }

    private void CollideWithObject(Collider2D collider)
    {
        if (collider.gameObject.layer == _platform)
        {
            UseDroppableAndPoolIt();
            return;
        }
        if (collider.gameObject.layer == _deathTrigger)
        {
            PoolDropable();
            return;
        }
    }
    private void UseDroppableAndPoolIt()
    {
        PlayItemSound();
        ActivateDroppableEffect();
        PoolDropable();
    }
    private void PoolDropable() => 
        _dropService.PoolDroppable(transform.parent.gameObject);

    private void ActivateDroppableEffect() => 
        _dropable.Use();

    private void PlayItemSound()
    {
        if (_itemSound)
        {
            _itemSound.Play();
        }
    }
}
