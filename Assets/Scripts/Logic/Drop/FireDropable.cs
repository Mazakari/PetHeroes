using UnityEngine;

public class FireDropable : MonoBehaviour, IDropableItem
{
    [SerializeField] private PlatformCollision _platformCollision;

    private void OnEnable() => 
        _platformCollision.SetDropableReference(this);

    public void ActivateDropable() => 
        _platformCollision.ActivateDropabable();

    public void ResetDropable() => 
        _platformCollision.ResetDropabable();

    public void Use() => 
        RespawnPlayer();

    private static void RespawnPlayer()
    {
        PlayerRespawn player = FindObjectOfType<PlayerRespawn>();

        if (player != null)
        {
            player.Respawn();
        }
    }
}
