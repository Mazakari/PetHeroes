using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Transform _playerRespawnPoint;

    [SerializeField] private LaunchPlayer _launchPlayer;
    [SerializeField] private VictimBasketsControl _basketsControl;

   private ILevelProgressService _levelProgressService;

    private void OnEnable() => 
        _levelProgressService = AllServices.Container.Single<ILevelProgressService>();

    public void SetRespawnPointReferrence(Transform respawnPoint) =>
     _playerRespawnPoint = respawnPoint;

    public void Respawn()
    {
        DecreasePlayerCurrentLivesCount();
        DeactivatePlayerBaskets();
        ResetPlayerPositionToRespawnPoint();
        ResetPlayerToWaitForLaunchState();
    }
    private void DecreasePlayerCurrentLivesCount() =>
        _levelProgressService.DecreasePlayerLives();
    private void DeactivatePlayerBaskets() =>
       _basketsControl.DeactivateBaskets();
    private void ResetPlayerPositionToRespawnPoint() => 
        transform.position = _playerRespawnPoint.position;
    private void ResetPlayerToWaitForLaunchState() => 
        _launchPlayer.InitRigidbodyAndStartDirection();
}
