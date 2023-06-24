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

    public static event Action<State> OnStateChange;

    void Start() => 
        InitState();

    private void Update() => 
        UpdateState();

    private void UpdateState()
    {
        float curAbsSpeedY = Mathf.Abs(_rb.velocity.y);

        if (curAbsSpeedY >= 0.01f)
        {
            _previous = _current;
            _current = State.JumpingUp;
        }

        if (curAbsSpeedY <= 0.01f)
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
