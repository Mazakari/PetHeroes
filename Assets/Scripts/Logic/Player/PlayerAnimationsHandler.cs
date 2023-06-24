using UnityEngine;

public class PlayerAnimationsHandler : MonoBehaviour
{
    [SerializeField] private PlayerAnimator _playerAnimator;

    private void OnEnable() => 
        PlayerState.OnStateChange += PlayStateAnimation;

    private void OnDisable() => 
        PlayerState.OnStateChange -= PlayStateAnimation;

    private void PlayStateAnimation(State state)
    {
        switch (state)
        {
            case State.GameStart:
                _playerAnimator.SetFallingDownSprite();
                break;

            case State.JumpingUp:
                _playerAnimator.SetJumpingUpSprite();
                break;

            case State.FallingDown:
                _playerAnimator.SetFallingDownSprite();
                break;

            case State.Win:
                _playerAnimator.SetWinSprite();
                break;

            default:
                Debug.LogError("No animation exist for this state");
                break;
        }
    }
}
