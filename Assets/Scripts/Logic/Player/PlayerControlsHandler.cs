using UnityEngine;

public class PlayerControlsHandler : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private SpriteRenderer _playerRenderer;
    [SerializeField] private Rigidbody2D _rb;

    private void OnEnable()
    {
        LevelState.OnLevelLoaded += DisableControls;
        LevelState.OnLevelStart += EnableControls;
        FinishPlatform.OnLevelFinish += DisableControls;
    }

    private void OnDisable()
    {
        LevelState.OnLevelStart -= EnableControls;
        FinishPlatform.OnLevelFinish -= DisableControls;
    }

    private void EnableControls()
    {
        _playerMovement.enabled = true;
        _rb.isKinematic = false;
    }

    private void DisableControls()
    {
        if (_playerMovement && _rb)
        {
            _playerMovement.enabled = false;
            _rb.isKinematic = true;

            _rb.velocity = Vector3.zero;
        }
    }
}
