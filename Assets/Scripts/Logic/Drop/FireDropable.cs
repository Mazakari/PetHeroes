using UnityEngine;

public class FireDropable : MonoBehaviour, IDropable
{
    [SerializeField] private PlatformCollision _platformCollision;

    private void OnEnable() => 
        _platformCollision.SetDropableReference(this);

    public void Activate()
    {
        PlayerRespawn player = FindObjectOfType<PlayerRespawn>();

        if (player != null)
        {
            player.Respawn();
        }
    }
}
