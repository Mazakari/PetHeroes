using System;
using UnityEngine;

public enum State
{
    GameStart,
    JumpingUp,
    FallingDown,
    Win
}

public class PlayerState : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;

    private State _current;
    private State _previous;

    public event Action<State> OnStateChange;

    void Start() => 
        InitState();

    private void Update() => 
        UpdateState();

    private void UpdateState()
    {
        float yVelocityNormalized = _rb.velocity.normalized.y;
        if (yVelocityNormalized >= 0)
        {
            _previous = _current;
            _current = State.JumpingUp;
        }

        if (yVelocityNormalized < 0)
        {
            _previous = _current;
            _current = State.FallingDown;
        }

        SendStateChangeCallback();
    }

    private void SendStateChangeCallback()
    {
        if (_previous != _current)
        {
            OnStateChange?.Invoke(_current);
        }
    }

    private void InitState()
    {
        _current = State.GameStart;
        _previous = _current;
    }

}
