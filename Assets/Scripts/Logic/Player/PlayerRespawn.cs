using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Transform _playerRespawnPoint;

    [SerializeField] private LaunchPlayer _launchPlayer;
    [SerializeField] private VictimBasketsControl _basketsControl;

   
    public void Respawn()
    {
        _basketsControl.DeactivateBaskets();
        transform.position = _playerRespawnPoint.position;
        _launchPlayer.InitRigidbodyAndStartDirection();
    }

    public void SetRespawnPointReferrence(Transform respawnPoint) =>
      _playerRespawnPoint = respawnPoint;
}
